﻿using MassTransit;
using Persistence.Chat.ChatEntites.Dtos;
using Domain;
using Domain.Entities;

namespace Carsharing.Consumers
{
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
                TopicAuthorId = messageDto.RoomInitializerId
            };

            await _ctx.Messages.AddAsync(message).ConfigureAwait(false);
            _ctx.SaveChanges();
        }
    }
}