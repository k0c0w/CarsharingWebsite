using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;
using System.Diagnostics;

namespace MinioConsumer.Services.PrimaryStorageSaver;

public class PrimaryStorageSaver
{
    private IMetadataRepository tempMetadataRepository;
    private IMetadataRepository primaryMetadataRepository;
    private IS3Service tempS3Service;
    private IS3Service primaryS3Service;
    private OperationRepository operationRepository;
    private ILogger<Exception> _exceptionLogger;


    public async Task MoveDataToPrimaryStorageAsync(Guid operationId, Guid metadataId)
    {
        try
        {
            var metadata = await tempMetadataRepository.GetById(metadataId);
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
            await primaryMetadataRepository.Add(metadata);

            await tempS3Service.RemoveFileFromBucketAsync(fileInfo.BucketName, fileInfo.ObjectName);
            await tempMetadataRepository.RemoveById(metadataId);
        }
        catch(Exception ex) 
        {
            _exceptionLogger.LogError(ex, "Exception while transition files from temp storages to primary.");

            operationRepository.UpdateOperationStatus(operationId, Models.OperationStatus.Failed);
        }
    }
}
