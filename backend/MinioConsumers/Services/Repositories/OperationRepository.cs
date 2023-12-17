using Microsoft.Extensions.Options;
using MinioConsumer.DependencyInjection.ConfigSettings;
using MinioConsumer.Models;
using StackExchange.Redis;
using System.Diagnostics;
using System.Reactive.Joins;
using System.Text.Json;
using FileInfo = MinioConsumer.Models.FileInfo;

namespace MinioConsumer.Services.Repositories;

public class OperationRepository
{
    private readonly IDatabase _db;
    private readonly IServer _server;

    public OperationRepository(IConnectionMultiplexer connectionMultiplexer, IOptions<RedisDbSettings> options)
    {
        _db = connectionMultiplexer.GetDatabase((int)RedisDatabaseId.OperationStatusTracking);
        var redisSettings = options.Value;
        _server = connectionMultiplexer.GetServer(redisSettings.Host, redisSettings.Port);
    }

    public async Task<Guid> CreateNewOperationAsync(Guid initializerUserId, string schemaName)
    {
        var operation = new OperationInfo
        {
            Id = Guid.NewGuid(),
            InitializerUserId = initializerUserId,
            OperationStatus = OperationStatus.Receiving,
            MetadataSchemaName = schemaName,
        };

        await _db.StringSetAsync(operation.Id.ToString(), Serilize(operation));

        return operation.Id;
    }
    
    public async Task<bool> IsOperationInitializedByUser(Guid operationId, Guid userId)
    {
        var info = await GetByIdAsync(operationId);
        if (info == null)
            return false;

        return info.InitializerUserId == userId;
    }

    public async Task<OperationInfo?> GetByIdAsync(Guid id)
    {
        var value = await _db.StringGetAsync(id.ToString());
        if (value.IsNull)
            return default;

        return Deserilize<OperationInfo?>(value.ToString());
    }

    public async Task UpdateOperationStatusAsync(Guid operationId, OperationStatus status)
    {
        // can not downgrade status
        Debug.Assert(status != OperationStatus.Receiving);

        var info = await GetByIdAsync(operationId);

        if (info is null) 
            return;

        info.OperationStatus = status;
        TimeSpan? expireTime = status switch
        {
            OperationStatus.Canceled or OperationStatus.Failed or OperationStatus.Completed => TimeSpan.FromMinutes(30),
            _ => default(TimeSpan?)
        };

        await _db.StringSetAsync(operationId.ToString(), Serilize(info), expireTime);
    }

    public async Task<OperationStatus?> GetOperationStatusAsync(Guid guid)
    {
        var info = await GetByIdAsync(guid);

        return info?.OperationStatus;
    }

    public Task<bool> KeyExistsAsync(Guid key)
    {
        return _db.KeyExistsAsync(key.ToString());
    }

    public async IAsyncEnumerable<Guid> IterThroughKeysAsync(CancellationToken ct = default)
    {
        await foreach(var key in _server.KeysAsync((int)RedisDatabaseId.OperationStatusTracking, "*", pageSize: 25, cursor: 0).WithCancellation(ct))
        {
            yield return Guid.Parse(key);
        }
    }

    private static string Serilize<T>(T obj) => JsonSerializer.Serialize(obj);
    private static T Deserilize<T>(string obj) => JsonSerializer.Deserialize<T>(obj);
}


// inforamation to store in temp db to track files and metadata recivied
// must be stored no longer than 1 hour after operation accessing
public class UploadInfo
{
    public Guid OperationId { get; init; }

    //metadata target schema
    public string Schema { get; init; }

    public int TargetFilesCount { get; init; }

    public int RecievedFiles { get; init; }

    public FileInfo? RecivedFile { get; init; } 

    public MetadataBase UploadMetadata { get; init; }

    public bool UploadFullyCompleted { get; init; }
}

// information about operation
// stored in redis, stores not longer than 1 hour 
public class OperationInfo
{
    public Guid Id { get; init; }

    public OperationStatus OperationStatus { get; set; }

    public string MetadataSchemaName { get; init; }

    //id of user who initialized operation
    public Guid InitializerUserId { get; init; }
}