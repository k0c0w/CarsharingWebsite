using MassTransit;
using Persistence.Chat;
using Persistence.Chat.ChatEntites.Dtos;
using Persistence.Chat.ChatEntites.DomainModels;

namespace Carsharing.Consumers
{
    public class ChatMessageConsumer : IConsumer<ChatMessageDto>
    {
        private readonly ChatContext _ctx;
            // todo: messageContext
        public ChatMessageConsumer(ChatContext context) 
        {
            _ctx = context;
        }


        public async Task Consume(ConsumeContext<ChatMessageDto> context)
        {
            var messageDto = context.Message;

            var message = new Message()
            {
                AuthorId = messageDto.RoomInitializerId,
                Text = messageDto.Text,
                Time = messageDto.Time,
                TopicAuthorId = messageDto.AuthorId
            };

            await _ctx.AddAsync(message).ConfigureAwait(false);
            await _ctx.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
