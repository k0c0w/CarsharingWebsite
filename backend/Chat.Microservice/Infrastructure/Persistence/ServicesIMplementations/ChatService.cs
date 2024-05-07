using Domain;
using Domain.Interfaces;
using Domain.Repositories;
using Services.Abstractions;

namespace Persistence.Services.Implementations;

public class ChatService(IMessageRepository messageRepository, ITopicRepository topicRepository) : IChatService
{
    private readonly ITopicRepository _topicRepository = topicRepository;
    private readonly IMessageRepository _messageRepository = messageRepository;

    public async Task AddToChatAsync(string chatName, ITopicSubscriber subscriber)
    {
        var topic = await _topicRepository.GetOrCreateTopicAsync(topicName: chatName);
        await topic.AddSubscriberAsync(subscriber);
    }

    public async Task<bool> ReceiveMessageAsync(SendMessageDto messageDto)
    {
        var chatInfo = messageDto.ChatInfo;
        var topic = _topicRepository.GetTopic(chatInfo.ChatName);
        if (topic is null)
            return false;

        Message message = new Message()
        {
            AuthorId = messageDto.SenderId,
            Text = messageDto.Text,
            Topic = chatInfo.ChatName,
            Time = DateTime.UtcNow,
        };

        if (chatInfo.IsPersistent && !string.IsNullOrEmpty(messageDto.SenderId))
        {
            await _messageRepository.AddAsync(message);
        }

        topic.BroadcastMessage(message);

        return true;
    }

    public async Task RemoveFromChatAsync(string chatName, ITopicSubscriber subscriber)
    {
        var topic = await _topicRepository.GetOrCreateTopicAsync(topicName: chatName);

        await topic.RemoveSubscriberAsync(subscriber);
        if (!await topic.AnySubscribersAsync())
        {
            await _topicRepository.RemoveTopicAsync(topicName: chatName);
        }
    }
}
