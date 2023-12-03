using MinioConsumer.Models;
using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;
using System.Diagnostics;

namespace MinioConsumer.Services.PrimaryStorageSaver;

public class PrimaryStorageSaver<TMetadata> where TMetadata : MetadataBase
{
    private readonly ITempMetadataRepository<TMetadata> tempMetadataRepository;
    private readonly IMetadataRepository<TMetadata> primaryMetadataRepository;
    private readonly IS3Service tempS3Service;
    private readonly IS3Service primaryS3Service;
    private readonly OperationRepository operationRepository;
    private readonly ILogger<Exception> _exceptionLogger;

    public PrimaryStorageSaver(ITempMetadataRepository<TMetadata> tempMetadataRepository, 
        IMetadataRepository<TMetadata> primaryMetadataRepository, 
        ITempS3Service tempS3Service, 
        IS3Service primaryS3Service, 
        OperationRepository operationRepository, 
        ILogger<Exception> exceptionLogger)
    {
        this.tempMetadataRepository = tempMetadataRepository;
        this.primaryMetadataRepository = primaryMetadataRepository;
        this.tempS3Service = tempS3Service;
        this.primaryS3Service = primaryS3Service;
        this.operationRepository = operationRepository;
        _exceptionLogger = exceptionLogger;
    }

    public async Task MoveDataToPrimaryStorageAsync(Guid operationId, Guid metadataId)
    {
        try
        {
            var metadata = await tempMetadataRepository.GetByIdAsync(metadataId);
            if (metadata == null)
            {
                throw new InvalidOperationException("Metadata was not found");
            }

            var fileInfo = metadata.LinkedFileInfo;
            Debug.Assert(fileInfo != null);

            var file = await tempS3Service.GetFileFromBucketAsync(fileInfo.BucketName, fileInfo.ObjectName);
            if (file == null)
            {
                throw new InvalidOperationException("File not found");
            }

            if (!await primaryS3Service.BucketExsistAsync(file.BucketName)) ;
            await primaryS3Service.CreateBucketAsync(file.BucketName);
            await primaryS3Service.PutFileInBucketAsync(file);
            await primaryMetadataRepository.AddAsync(metadata);

            await tempS3Service.RemoveFileFromBucketAsync(fileInfo.BucketName, fileInfo.ObjectName);
            await tempMetadataRepository.RemoveByIdAsync(metadataId);
        }
        catch(Exception ex) 
        {
            _exceptionLogger.LogError(ex, "Exception while transition files from temp storages to primary.");

            await operationRepository.UpdateOperationStatusAsync(operationId, Models.OperationStatus.Failed);
        }
    }
}
