using Minio;
using MinioConsumers.Services;

namespace MinioConsumer.Services;

public interface ITempS3Service : IS3Service
{
}

public class TempS3Service : S3Service, ITempS3Service
{
    public TempS3Service(IMinioClientFactory minioClient) : base(minioClient.CreateClient(true))
    {
    }
}
