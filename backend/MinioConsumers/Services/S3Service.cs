using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using MinioConsumer.Services;

namespace MinioConsumers.Services;

public class S3Service : IS3Service
{
    private readonly IMinioClient _minioClient;

    public S3Service(MinioClientFactory minioClient)
    {
        _minioClient = minioClient.CreateClient(false);
    }

    protected S3Service(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public Task CreateBucketAsync(string bucketName)
    {
        var args = new MakeBucketArgs()
            .WithBucket(bucketName);

        return _minioClient.MakeBucketAsync(args);
    }

    public Task RemoveBucketAsync(string bucketName)
    {
        var args = new RemoveBucketArgs()
            .WithBucket(bucketName);

        return _minioClient.RemoveBucketAsync(args);
    }

    public Task<bool> BucketExsistAsync(string bucketName)
    {
        var args = new BucketExistsArgs()
            .WithBucket(bucketName);

        return _minioClient.BucketExistsAsync(args);
    }

    public Task PutFileInBucketAsync(S3File file)
    {
        var args = new PutObjectArgs()
            .WithBucket(file.BucketName)
            .WithStreamData(file.ContentStream)
            .WithObject(file.Name)
            .WithObjectSize(file.ContentStream.Length)
            .WithContentType(file.ContentType);

        return _minioClient.PutObjectAsync(args);
    }

    public Task RemoveFileFromBucketAsync(string fileName, string bucketName)
    {
        var args = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName);

        return _minioClient.RemoveObjectAsync(args);
    }

    public async Task<S3File?> GetFileFromBucketAsync(string bucketName, string fileName)
    {
        var response = new MemoryStream();
        var args = new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithCallbackStream(stream => stream.CopyTo(response));

        try
        {
            var objInfo = await _minioClient.GetObjectAsync(args);

            response.Position = 0;
            return new S3File(fileName, bucketName, response, objInfo.ContentType);
        }
        catch (ObjectNotFoundException)
        {
            return default;
        }
        catch (BucketNotFoundException)
        {
            return default;
        }
    }
}