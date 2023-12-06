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
    };
}


public class RedisMetadataRepository<TMetadata> : ITempMetadataRepository<TMetadata> where TMetadata : MetadataBase
{
    private readonly IDatabase _db;
    
    public RedisMetadataRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _db = connectionMultiplexer.GetDatabase((int)RedisDatabaseId.TempMetadata);
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
        metadata.LinkedFileInfo = file;
        await AddAsync(metadata);
    }

    public async Task<bool> IsCompletedByIdAsync(Guid metadataGuid)
    {
        var metadata = await GetByIdAsync(metadataGuid);
        if (metadata is null)
            return false;
        
        return !(metadata.LinkedFileInfo is null);
    }

    public Task RemoveByIdAsync(Guid id)
        => _db.KeyDeleteAsync(GetInternalKey(id));

    private string GetInternalKey(Guid id) => $"{MetadataSchemas.Schemas[typeof(TMetadata)]}:{id}";

    public Task<IEnumerable<TMetadata>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}