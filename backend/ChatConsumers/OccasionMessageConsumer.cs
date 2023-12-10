using MassTransit;
using Persistence.Chat.ChatEntites.Dtos;

namespace ChatConsumers;

public class OccasionMessageConsumer : IConsumer<OccasionChatMessageDto>
{
    public Task Consume(ConsumeContext<OccasionChatMessageDto> context)
    {
        throw new NotImplementedException();
    }
}
