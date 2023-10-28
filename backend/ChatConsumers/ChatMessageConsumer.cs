using Domain;
using Domain.Entities;
using MassTransit;
using Persistence.Chat.ChatEntites.Dtos;
using Domain.Entities;
using Persistence.Chat;

namespace Carsharing.ChatHub;

public class ChatMessageConsumer : IConsumer<ChatMessageDto>
{
    private readonly IMessageUnitOfWork _messageUoW;
    public ChatMessageConsumer(IMessageUnitOfWork messageUnitOfWork) 
    {
        _messageUoW = messageUnitOfWork;
    }

    public async Task Consume(ConsumeContext<ChatMessageDto> context)
    {
        var messageDto = context.Message;

        var message = new Message()
        {
            AuthorId = messageDto.AuthorId,
            Text = messageDto.Text,
            Time = messageDto.Time,
            TopicAuthorId = messageDto.RoomInitializerId,
            IsFromManager = messageDto.IsAuthorManager,
        };

        await _messageUoW.MessageRepository.AddAsync(message).ConfigureAwait(false);
        await _messageUoW.SaveChangesAsync().ConfigureAwait(false);
    }
}
