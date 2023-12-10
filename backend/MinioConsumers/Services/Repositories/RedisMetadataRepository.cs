using Microsoft.Extensions.Options;
using MinioConsumer.DependencyInjection.ConfigSettings;
using MinioConsumer.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using FileInfo = MinioConsumer.Models.FileInfo;

namespace MinioConsumer.Services.Repositories;

internal static class MetadataSchemas
{
    public static IReadOnlyDictionary<Type, string> Schemas = new Dictionary<Type, string>()
    {
        [typeof(DocumentMetadata)] = "document",
        [typeof(OccasionAttachmentMetadata)] = "attachment"
    };
}

public class RedisMetadataRepository<TMetadata> : ITempMetadataRepository<TMetadata> where TMetadata : MetadataBase
{
    private readonly IDatabase _db;
    private readonly IServer _server;

    private object _lock = new object();
    private bool _resetNeeded =false;
    
    public RedisMetadataRepository(IConnectionMultiplexer connectionMultiplexer, IOptions<RedisDbSettings> options)
    {
        _db = connectionMultiplexer.GetDatabase((int)RedisDatabaseId.TempMetadata);
        var redisSettings = options.Value;
        _server = connectionMultiplexer.GetServer(redisSettings.Host, redisSettings.Port);
    }
    
    public async Task<bool> MetadataExistsByIdAsync(Guid id)
    {
        var result = await _db.StringGetAsync(GetInternalKey(id));
        return !result.IsNullOrEmpty;
    }

    public async Task<TMetadata?> GetByIdAsync(Guid id)
    {
        var redisValue = await _db.StringGetAsync(GetInternalKey(id));
        if (redisValue.IsNull)
            return default;
        var result = JsonConvert.DeserializeObject<TMetadata>(redisValue.ToString());
        return result;
    }

    public async Task<Guid> AddAsync(TMetadata metadata)
    {
        var value = JsonConvert.SerializeObject(metadata);
        await _db.StringSetAsync(GetInternalKey(metadata.Id), value);
        return metadata.Id;
    }

    public async Task UpdateFileInfoAsync(Guid metadataGuid, FileInfo file)
    {
        var metadata = await GetByIdAsync(metadataGuid);
        metadata.LinkedFileInfos.Add(file);
        await AddAsync(metadata);
    }

    public async Task<bool> IsCompletedByIdAsync(Guid metadataGuid)
    {
        var metadata = await GetByIdAsync(metadataGuid);
        if (metadata is null)
            return false;
        
        return (metadata.LinkedFileInfos.Count == metadata.LinkedMetadataCount);
    }

    public Task RemoveByIdAsync(Guid id)
        => _db.KeyDeleteAsync(GetInternalKey(id));

    private string GetInternalKey(Guid id) => $"{MetadataSchemas.Schemas[typeof(TMetadata)]}:{id}";

    public Task<IEnumerable<TMetadata>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TMetadata metadata)
    {
        throw new NotImplementedException();
    }

    public async IAsyncEnumerable<Guid> IterThroughKeysAsync()
    {
        var pattern = $"{MetadataSchemas.Schemas[typeof(TMetadata)]}:*";

        await foreach (var key in _server.KeysAsync((int)RedisDatabaseId.TempMetadata, pattern, pageSize: 25, cursor: 0))
        {
            if (_resetNeeded)
                break;

            if (Guid.TryParse(key.ToString()?.Substring(0, MetadataSchemas.Schemas[typeof(TMetadata)].Length + 1), out Guid guid))
                yield return guid;

            break;
        }
    }

    public async Task StopIterationAsync()
    {
        _resetNeeded = true;

        await foreach (var key in IterThroughKeysAsync())
        {
            break;
        }
    }
}