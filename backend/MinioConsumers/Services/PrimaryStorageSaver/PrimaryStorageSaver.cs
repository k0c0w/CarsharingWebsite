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

            var fileInfo = metadata.LinkedFileInfo;
            Debug.Assert(fileInfo != null);

            var tempS3file = await s3Service.GetFileFromBucketAsync(fileInfo.BucketName, fileInfo.ObjectName);
            if (tempS3file == null)
            {
                throw new InvalidOperationException("File not found");
            }

            if (!await s3Service.BucketExsistAsync(fileInfo.TargetBucketName))
                await s3Service.CreateBucketAsync(fileInfo.TargetBucketName);
            await s3Service.PutFileInBucketAsync(new S3File
            (
                fileInfo.ObjectName,
                fileInfo.TargetBucketName,
                tempS3file.ContentStream,
                tempS3file.ContentType
            ));
            await primaryMetadataRepository.AddAsync(metadata);

            await s3Service.RemoveFileFromBucketAsync(tempS3file.BucketName, tempS3file.Name);
            await tempMetadataRepository.RemoveByIdAsync(metadataId);
        }
        catch(Exception ex) 
        {
            _exceptionLogger.LogError(ex, "Exception while transition files from temp storages to primary.");

            await operationRepository.UpdateOperationStatusAsync(operationId, Models.OperationStatus.Failed);
        }
    }
}
