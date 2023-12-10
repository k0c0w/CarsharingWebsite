using MinioConsumer.Models;
using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;

namespace MinioConsumer.BackgroundServices;

public class TempFileCleanerBackgroundService : BackgroundService
{

    private readonly IServiceProvider _serviceProvider;

    public TempFileCleanerBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IMetadataScopedProcessingService scopedProcessingService =
                    scope.ServiceProvider.GetRequiredService<IMetadataScopedProcessingService>();

                await scopedProcessingService.CleanTempFoldersAsync(stoppingToken);
            }

            await Task.Delay(TimeSpan.FromHours(4), stoppingToken);
        }
    }
}

public class TempFileCleanerScopedProcessingService : IMetadataScopedProcessingService
{
    private readonly IS3Service _s3Service;
    private readonly ITempMetadataRepository<DocumentMetadata> _tempDocumentMetadataRepository;
    private readonly ITempMetadataRepository<OccasionAttachmentMetadata> _tempAttachmentMetadataRepository;
    private readonly OperationRepository operationStatusRepository;
    private readonly ILogger<Exception> _logger;

    public TempFileCleanerScopedProcessingService(IS3Service s3Service, ITempMetadataRepository<DocumentMetadata> tempMetadataRepository,
        ITempMetadataRepository<OccasionAttachmentMetadata> tempAttachmentMetadataRepository, OperationRepository operationRepository, ILogger<Exception> logger)
    {
        _s3Service = s3Service;
        _tempDocumentMetadataRepository = tempMetadataRepository;
        _tempAttachmentMetadataRepository = tempAttachmentMetadataRepository;
        _tempDocumentMetadataRepository = tempMetadataRepository;
        _logger = logger;
        operationStatusRepository = operationRepository;
    }

    public Task CleanTempFoldersAsync(CancellationToken cancellationToken)
    {
        var tasks = new Task[]
        {
            DeleteTempAttachmentsAsync(),
            DeleteTempDocumentsAsync(),
        };

        return Task.WhenAll(tasks);
    }

    private async Task DeleteTempDocumentsAsync()
    {
        var operationStartTime = DateTime.UtcNow;
        var period = TimeSpan.FromHours(4);
        try
        {
            await foreach (var key in _tempDocumentMetadataRepository.IterThroughKeysAsync())
            {
                var metadata = await _tempDocumentMetadataRepository.GetByIdAsync(key);
                if (metadata is null)
                    continue;

                if (operationStartTime - metadata.CreationDateTimeUtc > period)
                {
                    foreach (var fileInfo in metadata.LinkedFileInfos)
                    {
                        try
                        {
                            await _s3Service.RemoveFileFromBucketAsync(fileInfo.BucketName, fileInfo.ObjectName);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, ex.Message);
                            await Task.Delay(5000);
                        }
                    }
                    try
                    {
                        await _tempDocumentMetadataRepository.RemoveByIdAsync(key);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }

                    if (await operationStatusRepository.KeyExistsAsync(key))
                        await operationStatusRepository.UpdateOperationStatusAsync(key, OperationStatus.Canceled);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        finally
        {
            await _tempDocumentMetadataRepository.StopIterationAsync();

        }
    }

    private async Task DeleteTempAttachmentsAsync()
    {
        var operationStartTime = DateTime.UtcNow;
        var period = TimeSpan.FromHours(4);
        try
        {
            await foreach (var key in _tempAttachmentMetadataRepository.IterThroughKeysAsync())
            {
                var metadata = await _tempAttachmentMetadataRepository.GetByIdAsync(key);
                if (metadata is null)
                    continue;

                if (operationStartTime - metadata.CreationDateTimeUtc > period)
                {
                    foreach (var fileInfo in metadata.LinkedFileInfos)
                    {
                        try
                        {
                            await _s3Service.RemoveFileFromBucketAsync(fileInfo.BucketName, fileInfo.ObjectName);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, ex.Message);
                            await Task.Delay(5000);
                        }
                    }
                    try
                    {
                        await _tempAttachmentMetadataRepository.RemoveByIdAsync(key);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }

                    if (await operationStatusRepository.KeyExistsAsync(key))
                        await operationStatusRepository.UpdateOperationStatusAsync(key, OperationStatus.Canceled);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        finally
        {
            await _tempAttachmentMetadataRepository.StopIterationAsync();

        }
    }
}


public interface IMetadataScopedProcessingService
{
    Task CleanTempFoldersAsync(CancellationToken cancellationToken);
}