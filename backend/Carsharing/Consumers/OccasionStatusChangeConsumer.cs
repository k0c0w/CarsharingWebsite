using Contracts.Occasion;
using MassTransit;

namespace Carsharing.Consumers;

public class OccasionStatusChangeConsumer : IConsumer<OccasionStatusChangeDto>
{
    private ILogger<Exception> _logger;


    public async Task Consume(ConsumeContext<OccasionStatusChangeDto> context)
    {
        var occasionStatusChangeDto = context.Message;

        try
        {
            var task = occasionStatusChangeDto.ChangeType switch
            {
                OccasionStatusChange.Created => OnCreationAsync(occasionStatusChangeDto.OccasionId),
                OccasionStatusChange.Completed => OnCompletionAsync(occasionStatusChangeDto.OccasionId),
                _ => Task.CompletedTask,
            };

            await task;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }

    private Task OnCreationAsync(Guid occasionId)
    {
        //todo: proccess status change

        return Task.CompletedTask;
    }

    private Task OnCompletionAsync(Guid occasionId)
    {
        //todo: proccess status change

        return Task.CompletedTask;
    }
}
