using Contracts;
using MassTransit;
using MinioConsumers.Services;

namespace MinioConsumers;

public class MinioConsumer: IConsumer<SaveImageDto>
{
    private readonly IS3Service _storageService;

    public MinioConsumer(IS3Service storageService)
    {
        _storageService = storageService;
    }

    public async Task Consume(ConsumeContext<SaveImageDto> context)
    {
        var stream = new MemoryStream(context.Message.Image);
        var bucketExist = await _storageService.BucketExsistAsync(context.Message.BucketName);

        if (!bucketExist)
        {
            await _storageService.CreateBucketAsync(context.Message.BucketName);
        }

        await _storageService.PutFileInBucketAsync(stream, context.Message.Name, context.Message.BucketName);
    }
}