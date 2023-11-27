using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace MinioConsumers.Services;

public class S3Service : IS3Service
{
    private readonly IMinioClient _minioClient;

    public S3Service(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task CreateBucketAsync(string bucketName)
    {
        var args = new MakeBucketArgs().WithBucket(bucketName);

        await _minioClient.MakeBucketAsync(args);
    }

    public async Task RemoveBucketAsync(string bucketName)
    {
        var args = new RemoveBucketArgs().WithBucket(bucketName);

        await _minioClient.RemoveBucketAsync(args);
    }

    public async Task<bool> BucketExsistAsync(string bucketName)
    {
        var args = new BucketExistsArgs()
            .WithBucket(bucketName);

        return await _minioClient.BucketExistsAsync(args);
    }

    public async Task PutFileInBucketAsync(Stream stream, string fileName, string bucketName)
    {
        var args = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithStreamData(stream)
            .WithObject(fileName)
            .WithContentType("image/x-png")
            .WithObjectSize(stream.Length);

        await _minioClient.PutObjectAsync(args);
    }

    public async Task RemoveFileFromBucketAsync(string fileName, string bucketName)
    {
        var args = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName);

        await _minioClient.RemoveObjectAsync(args);
    }

    public async Task<Stream> GetFileFromBucketAsync(string fileName, string bucketName)
    {
        var response = new MemoryStream();
        var args = new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithCallbackStream(stream => stream.CopyTo(response));

        try
        {
            await _minioClient.GetObjectAsync(args);
        }
        catch (ObjectNotFoundException)
        {
            return null;
        }
        catch (BucketNotFoundException)
        {
            return null;
        }

        response.Position = 0;
        return response;
    }
}