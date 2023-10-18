using MassTransit;
using Persistence.Chat.ChatEntites.Dtos;
using Domain.Entities;
using Migrations.CarsharingApp;
using Migrations.Chat;

namespace Carsharing.Consumers;

public class ChatMessageConsumer : IConsumer<ChatMessageDto>
{
    private readonly ChatContext _ctx;
    public ChatMessageConsumer(ChatContext context) 
    {
        _ctx = context;
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

        await _ctx.Messages.AddAsync(message).ConfigureAwait(false);
        _ctx.SaveChanges();
    }
}
