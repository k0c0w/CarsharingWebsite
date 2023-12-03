using MinioConsumer.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using FileInfo = MinioConsumer.Models.FileInfo;

namespace MinioConsumer.Services.Repositories;

public class MetadataRepository : IMetadataRepository
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    
    public MetadataRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }
    
    public async Task<bool> MetadataExists(Guid guid)
    {
        var result = await _connectionMultiplexer.GetDatabase().StringGetAsync(guid.ToString());
        return !result.IsNullOrEmpty;
    }

    public async Task<MetadataBase?> GetById(Guid id)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var redisValue = await db.StringGetAsync(id.ToString());
        var result = JsonConvert.DeserializeObject<MetadataBase>(redisValue);
        return result;
    }

    public async Task<MetadataBase> Add(MetadataBase metadata)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var value = JsonConvert.SerializeObject(metadata);
        var redisValue = await db.StringSetAndGetAsync(metadata.Id.ToString(), value);
        return metadata;
    }

    public async Task UpdateFileInfo(Guid metadataGuid, FileInfo file)
    {
        var metadata = await GetById(metadataGuid);
        metadata.LinkedFileInfo = file;
        await Add(metadata);
    }

    public async Task<bool> IsCompletedById(Guid metadataGuid)
    {
        var metadata = await GetById(metadataGuid);
        if (metadata is null)
            return false;
        
        return !(metadata.LinkedFileInfo is null);
    }

    public async Task<MetadataBase> RemoveById(Guid guid)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var metadata = await GetById(guid);
        var redisValue = await db.KeyDeleteAsync(guid.ToString());
        return metadata;
    }
}