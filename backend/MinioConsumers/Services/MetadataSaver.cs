﻿using MinioConsumer.Models;
using MinioConsumer.Services.PrimaryStorageSaver;
using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;
using Shared.Results;
using FileInfo = MinioConsumer.Models.FileInfo;

namespace MinioConsumer.Services;

public class MetadataSaver<TMetadata> where TMetadata : MetadataBase
{
    private readonly ILogger<Exception> _exceptionLogger;

    private readonly ITempMetadataRepository<TMetadata> metadataRepository;

    private readonly IS3Service s3Service;

    private readonly PrimaryStorageSaver<TMetadata> operationRepository;

    public MetadataSaver(ILogger<Exception> exceptionLogger, ITempMetadataRepository<TMetadata> metadataRepository, ITempS3Service s3Service, PrimaryStorageSaver<TMetadata> operationRepository)
    {
        _exceptionLogger = exceptionLogger;
        this.metadataRepository = metadataRepository;
        this.s3Service = s3Service;
        this.operationRepository = operationRepository;
    }

    /// <summary>
    /// Returns operation status
    /// </summary>
    /// <param name="metadata"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<Result> UploadFile(Guid operationId, TMetadata metadata, IFormFile file) 
    {
        var metadaResult = await UploadFileAsync(operationId, metadata);
        if (!metadaResult)
            return metadaResult;

        return await AppendFileAsync(operationId, file);
    }

    public async Task<Result> AppendFileAsync(Guid operationId, IFormFile file) 
    {
        if (!await metadataRepository.MetadataExistsByIdAsync(operationId))
            return Result.ErrorResult;

        if (await metadataRepository.IsCompletedByIdAsync(operationId))
            return Result.ErrorResult;

        var tempBucketName = KnownBuckets.GetRandomBucketFromPool();
        var bucketObjectName = Guid.NewGuid().ToString();

        var info = new FileInfo
        {
            BucketName = tempBucketName,
            ContentType = file.ContentType,
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
            .ContinueWith( savingTask => metadataRepository.UpdateFileInfoAsync(operationId, info), TaskContinuationOptions.OnlyOnRanToCompletion);
        }
        catch(Exception ex)
        {
            _exceptionLogger.LogError(ex, $"Error while saving temp file with operation: {operationId}");
            return Result.ErrorResult;
        }

        return Result.SuccessResult; 
    }

    public async Task<Result> UploadFileAsync(Guid operationId, TMetadata metadata) 
    {
        if (!await metadataRepository.MetadataExistsByIdAsync(operationId) || await metadataRepository.IsCompletedByIdAsync(operationId))
            return Result.ErrorResult;

        await metadataRepository.AddAsync(metadata);
        return Result.SuccessResult;
    }

    public async Task<Result<bool>> IsOperationCompleted(Guid id)
    {
        var completed = (await metadataRepository.MetadataExistsByIdAsync(id))
            && (await metadataRepository.IsCompletedByIdAsync(id));

        return new Ok<bool>(completed);
    }

    public async Task<Result> CommitOperationAsync(Guid operationId)
    {
        try
        {
            if (await metadataRepository.MetadataExistsByIdAsync(operationId))
            {
                _ = Task.Run(() => operationRepository.MoveDataToPrimaryStorageAsync(operationId, operationId));
                return Result.SuccessResult;
            }
            return Result.ErrorResult;
        }
        catch
        {
            return Result.ErrorResult;
        }
    }
}