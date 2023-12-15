using Microsoft.Extensions.Options;
using MinioConsumer.DependencyInjection.ConfigSettings;
using MinioConsumer.Services.PrimaryStorageSaver;
using MinioConsumer.Services.Repositories;
using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MinioConsumer.BackgroundServices;

public class BackgroundSaver : BackgroundService
{
    private readonly ILogger<BackgroundSaver> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IServer _redisServer;
    private readonly IDatabase _redisDb;

    public BackgroundSaver(IBackgroundTaskQueue taskQueue,
        ILogger<BackgroundSaver> logger,
        IServiceProvider serviceProvider,
        IConnectionMultiplexer connectionMultiplexer,
        IOptions<RedisDbSettings> options)
    {
        TaskQueue = taskQueue;
        _logger = logger;
        var redisSettings = options.Value;
        _redisServer = connectionMultiplexer.GetServer(redisSettings.Host, redisSettings.Port);
        _serviceProvider = serviceProvider;
        _redisDb = connectionMultiplexer.GetDatabase((int)RedisDatabaseId.OperationStatusTracking);
    }

    public IBackgroundTaskQueue TaskQueue { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var proccessedFromRedis = 0;
        var redisKeysAsyncEnumerator = GetOperationsKeysAsyncEnumerator(stoppingToken);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (proccessedFromRedis == 2)
                {
                    proccessedFromRedis = 0;

                    var key = await TaskQueue.DequeueOperationIdToSaveAsync(stoppingToken);
                    if (stoppingToken.IsCancellationRequested || key is null)
                        continue;

                    if (key != default)
                        await BackgroundProcessing(key.Value);
                }
                else
                {
                    proccessedFromRedis++;
                    var moveNext = await redisKeysAsyncEnumerator.MoveNextAsync();

                    var key = redisKeysAsyncEnumerator.Current;

                     await BackgroundProcessing(key!);

                    if (!moveNext)
                        redisKeysAsyncEnumerator = GetOperationsKeysAsyncEnumerator(stoppingToken);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }

    private static readonly Type _openPrimaryStorageSaverType = typeof(PrimaryStorageSaver<>);
    private const string entryPointName = "MoveDataToPrimaryStorageAsync";

    private async Task BackgroundProcessing(Guid id)
    {
        var operationInfo = JsonSerializer.Deserialize<OperationInfo?>((await _redisDb.StringGetAsync(id.ToString())).ToString() ?? string.Empty);
        if (operationInfo is null || operationInfo.OperationStatus != Models.OperationStatus.InProggress)
            return;

        var metadataType = MetadataSchemas.Schemas
            .Where(x => x.Value == operationInfo.MetadataSchemaName)
            .Select(x => x.Key)
            .FirstOrDefault();
        if (metadataType == null)
        {
            _logger.LogInformation($"Attempt to save unsupported schema: {operationInfo.MetadataSchemaName}");
            return;
        }
        try
        {
            var primarySaverType = _openPrimaryStorageSaverType.MakeGenericType(metadataType);
            using var scope = _serviceProvider.CreateScope();

            var primarySaver = _serviceProvider.GetService(primarySaverType);

            await(primarySaverType.GetMethod(entryPointName).Invoke(primarySaver, new object[] { id, id }) as Task);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occured while saving metadada!");
        }
    }

    private async IAsyncEnumerator<Guid> GetOperationsKeysAsyncEnumerator(CancellationToken ct)
    {
        await foreach (var key in _redisServer.KeysAsync((int)RedisDatabaseId.OperationStatusTracking, "*", pageSize: 25).WithCancellation(ct))
            yield return Guid.Parse(key);
    }
}

public interface IBackgroundTaskQueue
{
    void QueueOperationIdToSave(Guid id);

    Task<Guid?> DequeueOperationIdToSaveAsync(CancellationToken ct);
}

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly ConcurrentQueue<Guid> _queue = new ConcurrentQueue<Guid>();

    public void QueueOperationIdToSave(Guid id)
    { 
        _queue.Enqueue(id);
    }

    public async Task<Guid?> DequeueOperationIdToSaveAsync(CancellationToken ct)
    {
        Guid result;
        while (!_queue.TryDequeue(out result))
        {
            if (ct.IsCancellationRequested)
                return null;

            await Task.Delay(100);
        }

        Debug.Assert(result != Guid.Empty);

        return result;
    }
}