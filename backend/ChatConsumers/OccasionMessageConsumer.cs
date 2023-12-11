using Entities.Entities;
using MassTransit;
using Persistence.Chat.ChatEntites.Dtos;
using Persistence.RepositoryImplementation;

namespace ChatConsumers;

public class OccasionMessageConsumer : IConsumer<OccasionChatMessageDto>
{
    private readonly OccasionMessageRepository _occasionMessageRepository;
    public OccasionMessageConsumer(OccasionMessageRepository occasionMessageRepository)
    {
        _occasionMessageRepository = occasionMessageRepository;
    }
    public async Task Consume(ConsumeContext<OccasionChatMessageDto> context)
    {
        var messageDto = context.Message;

        var message = new OccasionMessage()
        {
            Attachment = messageDto.Attachment,
            AuthorId = messageDto.AuthorId,
            OccasionId = messageDto.OccasionId,
            Id = messageDto.Guid,
            Text = messageDto.Text,
            Time = messageDto.Time,
            IsFromManager = messageDto.IsAuthorManager,
        };

        await _occasionMessageRepository.AddAsync(message).ConfigureAwait(false);
    }
}
