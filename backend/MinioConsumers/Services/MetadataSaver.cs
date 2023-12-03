using MinioConsumer.Models;
using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;
using Shared.Results;
using System.Diagnostics;
using FileInfo = MinioConsumer.Models.FileInfo;

namespace MinioConsumer.Services;

public class MetadataSaver
{
    private static string[] bucketPool = new []{ "zebra", "mongoose", "elephant", "monkey", "pantera" };

    private readonly ILogger<Exception> _exceptionLogger;

    private readonly IMetadataRepository metadataRepository;

    private readonly IS3Service s3Service;

    public MetadataSaver(ILogger<Exception> exceptionLogger, IMetadataRepository metadataRepository, IS3Service s3Service)
    {
        _exceptionLogger = exceptionLogger;
        this.metadataRepository = metadataRepository;
        this.s3Service = s3Service;
    }

    /// <summary>
    /// Returns operation status
    /// </summary>
    /// <param name="metadata"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<Result> UploadFile(Guid operationId, MetadataBase metadata, IFormFile file) 
    {
        var metadaResult = await UploadFileAsync(operationId, metadata);
        if (!metadaResult)
            return metadaResult;

        return await AppendFileAsync(operationId, file);
    }

    public async Task<Result> AppendFileAsync(Guid operationId, IFormFile file) 
    {
        if (!await metadataRepository.MetadataExists(operationId))
            return Result.ErrorResult;

        if (await metadataRepository.IsCompletedById(operationId))
            return Result.ErrorResult;

        var rand = new Random();

        var tempBucketName = bucketPool[rand.Next(bucketPool.Length)];
        var bucketObjectName = Guid.NewGuid().ToString();

        var info = new FileInfo
        {
            BucketName = tempBucketName,
            ContentType = file.ContentType,
            IsTemporary = true,
            ObjectName = bucketObjectName,
            OriginalFileName = file.FileName,
        };

        try
        {
            using var fileStream = file.OpenReadStream();

            await s3Service.BucketExsistAsync(tempBucketName)
            .ContinueWith(async (existanceTask) =>
            {
                if (!existanceTask.Result)
                    await s3Service.CreateBucketAsync(tempBucketName);
                await s3Service.PutFileInBucketAsync(new S3File(bucketObjectName, tempBucketName, fileStream, file.ContentType));
            },
            TaskContinuationOptions.OnlyOnRanToCompletion)
            .ContinueWith( savingTask => metadataRepository.UpdateFileInfo(operationId, info), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        catch(Exception ex)
        {
            _exceptionLogger.LogError(ex, $"Error while saving temp file with operation: {operationId}");
            return Result.ErrorResult;
        }

        return Result.SuccessResult; 
    }

    public async Task<Result> UploadFileAsync(Guid operationId, MetadataBase metadata) 
    {
        Debug.Assert(operationId == metadata.Id);

        if (!await metadataRepository.MetadataExists(operationId) || await metadataRepository.IsCompletedById(operationId))
            return Result.ErrorResult;

        await metadataRepository.Add(metadata);
        return Result.SuccessResult;
    }

    public async Task<Result<bool>> IsOperationCompleted(Guid id)
    {
        var completed = (await metadataRepository.MetadataExists(id))
            && (await metadataRepository.IsCompletedById(id));

        return new Ok<bool>(completed);
    }

    // call another service here
    public Result CommitOperation(Guid operationId)
    {
        //todo: initilize saving to mongo and main s3 service
        // on complete it should update operation status
        throw new NotImplementedException();
    }
}
