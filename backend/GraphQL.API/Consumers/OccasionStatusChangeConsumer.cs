using Contracts.Occasion;
using MassTransit;
using Persistence.Chat.ChatEntites;

namespace GraphQL.API.Consumers;

public class OccasionStatusChangeConsumer : IConsumer<OccasionStatusChangeDto>
{
    private readonly ILogger<Exception> _logger;
    private readonly OccasionChatRepository _occasionRepository;

    public OccasionStatusChangeConsumer(ILogger<Exception> logger, OccasionChatRepository repo)
    {
        _logger = logger;
        _occasionRepository = repo;
    }

    public async Task Consume(ConsumeContext<OccasionStatusChangeDto> context)
    {
        var occasionStatusChangeDto = context.Message;

        try
        {
            var task = occasionStatusChangeDto.ChangeType switch
            {
                OccasionStatusChange.Created => OnCreationAsync(occasionStatusChangeDto.OccasionId, occasionStatusChangeDto.IssuerId),
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

    private Task OnCreationAsync(Guid occasionId, Guid issuerId)
    {
        _occasionRepository.TryAddRoom(occasionId, issuerId);

        return Task.CompletedTask;
    }

    private Task OnCompletionAsync(Guid occasionId)
    {
        _occasionRepository.TryRemoveRoom(occasionId);
        return Task.CompletedTask;
    }
}
