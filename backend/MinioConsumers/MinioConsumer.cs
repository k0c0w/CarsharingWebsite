using Contracts;
using MassTransit;
using MinioConsumers.Services;

namespace MinioConsumers;

public class MinioConsumer : IConsumer<SaveCarModelImageDto>
{
    private readonly IS3Service _storageService;
    private readonly IBus _bus;

    public MinioConsumer(IS3Service storageService, IBus bus)
    {
        _bus = bus;
        _storageService = storageService;
    }

    public async Task Consume(ConsumeContext<SaveCarModelImageDto> context)
    {
        var message = context.Message;
        var stream = new MemoryStream(message.Image);
        var bucketExist = await _storageService.BucketExsistAsync("models");

        if (!bucketExist)
        {
            await _storageService.CreateBucketAsync("models");
        }

        try
        {
            var url = await _storageService.PutFileInBucketAsync(stream, "models" , message.ImageName);

            await _bus.Publish(new CarModelImageSaveResultDto() { Success = true, CarModelId = message.CarModelId, Url = url });
        }
        catch
        {
            await _bus.Publish(new CarModelImageSaveResultDto() { Success = false, CarModelId = message.CarModelId });
        }
    }
}