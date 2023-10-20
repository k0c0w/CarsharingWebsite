using Domain;
using Domain.Entities;
using MassTransit;
using Persistence.Chat.ChatEntites.Dtos;

namespace Carsharing.ChatHub;

public class ChatMessageConsumer : IConsumer<ChatMessageDto>
{
    private readonly CarsharingContext _ctx;
    public ChatMessageConsumer(CarsharingContext context) 
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
        await _ctx.SaveChangesAsync();
    }
}
