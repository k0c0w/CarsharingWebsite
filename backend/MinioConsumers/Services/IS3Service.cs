namespace MinioConsumers.Services;

public interface IS3Service
{
    public Task CreateBucketAsync(string bucketName);
    public Task RemoveBucketAsync(string bucketName);
    public Task<bool> BucketExsistAsync(string bucketName);
    public Task PutFileInBucketAsync( Stream stream,string fileName, string bucketName);
    public Task RemoveFileFromBucketAsync(string fileName,string bucketName);
    public Task<Stream> GetFileFromBucketAsync(string fileName,string bucketName);
}