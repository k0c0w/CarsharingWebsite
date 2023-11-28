using MinioConsumer.Services;

namespace MinioConsumers.Services;

public interface IS3Service
{
    public Task CreateBucketAsync(string bucketName);

    public Task RemoveBucketAsync(string bucketName);

    public Task<bool> BucketExsistAsync(string bucketName);

    public Task PutFileInBucketAsync(S3File file);

    public Task RemoveFileFromBucketAsync(string bucketName, string fileName);

    public Task<S3File?> GetFileFromBucketAsync(string bucketName, string fileName);
}