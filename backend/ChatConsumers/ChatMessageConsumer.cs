using Domain.Entities;
using MassTransit;
using Persistence.Chat.ChatEntites.Dtos;
using Persistence.Chat;

namespace Carsharing.ChatHub;

public class ChatMessageConsumer : IConsumer<ChatMessageDto>
{
    private readonly IMessageUnitOfWork _messageUoW;
    private readonly ILogger<ChatMessageConsumer> _logger;

    public ChatMessageConsumer(IMessageUnitOfWork messageUnitOfWork, ILogger<ChatMessageConsumer> logger) 
    {
        _logger = logger;
        _messageUoW = messageUnitOfWork;
    }

    public async Task Consume(ConsumeContext<ChatMessageDto> context)
    {
        var messageDto = context.Message;

        _logger.LogInformation("Got new message {message}", messageDto.Text);

        var message = new Message()
        {
            AuthorId = messageDto.AuthorId,
            Text = messageDto.Text,
            Time = messageDto.Time,
            TopicAuthorId = messageDto.RoomInitializerId,
            IsFromManager = messageDto.IsAuthorManager,
        };

        await _messageUoW.MessageRepository.AddAsync(message);
        await _messageUoW.SaveChangesAsync();
    }
}
