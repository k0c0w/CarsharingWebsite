using MinioConsumer.Models;
using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;
using System.Diagnostics;

namespace MinioConsumer.Services.PrimaryStorageSaver;

public class PrimaryStorageSaver<TMetadata> where TMetadata : MetadataBase
{
    private readonly ITempMetadataRepository<TMetadata> tempMetadataRepository;
    private readonly IMetadataRepository<TMetadata> primaryMetadataRepository;
    private readonly IS3Service s3Service;
    private readonly OperationRepository operationRepository;
    private readonly ILogger<Exception> _exceptionLogger;

    public PrimaryStorageSaver(ITempMetadataRepository<TMetadata> tempMetadataRepository, 
        IMetadataRepository<TMetadata> primaryMetadataRepository, 
        IS3Service s3Service, 
        OperationRepository operationRepository, 
        ILogger<Exception> exceptionLogger)
    {
        this.tempMetadataRepository = tempMetadataRepository;
        this.primaryMetadataRepository = primaryMetadataRepository;
        this.s3Service = s3Service;
        this.operationRepository = operationRepository;
        _exceptionLogger = exceptionLogger;
    }

    public async Task MoveDataToPrimaryStorageAsync(Guid operationId, Guid metadataId)
    {
        try
        {
            await operationRepository.UpdateOperationStatusAsync(operationId, OperationStatus.InProggress);

            var metadata = await tempMetadataRepository.GetByIdAsync(metadataId);
            if (metadata == null)
            {
                throw new InvalidOperationException("Metadata was not found");
            }

            var fileInfos = metadata.LinkedFileInfos;
            Debug.Assert(fileInfos.Count == metadata.LinkedMetadataCount);

            foreach (var bucket in fileInfos
                                    .Select(x => x.TargetBucketName)
                                    .Distinct()
                                    )
            {
                if (!await s3Service.BucketExsistAsync(bucket))
                    await s3Service.CreateBucketAsync(bucket);
            }
            var tasks = new Task[fileInfos.Count];
            var tempS3Files = new S3File[fileInfos.Count];
            for (var i = 0; i < fileInfos.Count; i++)
            {
                var index = i;
                var fileInfo = fileInfos[i];
                tasks[i] = Task.Run(async () =>
                {
                    var tempS3File = await s3Service.GetFileFromBucketAsync(fileInfo.BucketName, fileInfo.ObjectName);
                    if (tempS3File == null)
                    {
                        throw new InvalidOperationException("File not found");
                    }
                    tempS3Files[index] = tempS3File!;

                    await s3Service.PutFileInBucketAsync(new S3File
                    (
                        fileInfo.ObjectName,
                        fileInfo.TargetBucketName,
                        tempS3File.ContentStream,
                        tempS3File.ContentType
                    ));
                });
            }

            await Task.WhenAll(tasks);
            if (tasks.Any(x => x.IsFaulted))
                throw new InvalidOperationException($"One of coping files faulted metadataId ({metadata.Id})");
            await primaryMetadataRepository.AddAsync(metadata);

            for (var i = 0; i < fileInfos.Count; i++)
            {
                var fileInfo = fileInfos[i];
                var tempS3File = tempS3Files[i];
                tasks[i] = s3Service.RemoveFileFromBucketAsync(tempS3File.BucketName, tempS3File.Name); 
            }
            await Task.WhenAll(tasks);
            await tempMetadataRepository.RemoveByIdAsync(metadataId);
        }
        catch(Exception ex) 
        {
            _exceptionLogger.LogError(ex, "Exception while transition files from temp storages to primary.");

            await operationRepository.UpdateOperationStatusAsync(operationId, Models.OperationStatus.Failed);
        }
    }
}
