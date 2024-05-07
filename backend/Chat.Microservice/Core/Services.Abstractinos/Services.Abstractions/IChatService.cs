using Domain;
using Domain.Interfaces;

namespace Services.Abstractions;

public interface IChatService
{
    Task AddToChatAsync(string chatName, ITopicSubscriber subscriber);

    Task RemoveFromChatAsync(string chatName, ITopicSubscriber subscriber);

    Task<bool> ReceiveMessageAsync(SendMessageDto messageDto);
}
