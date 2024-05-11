using Domain;
using Domain.Interfaces;
using Domain.Repositories;
using Services.Abstractions;

namespace Persistence.Services.Implementations;

public class ChatService(IMessageRepository messageRepository, ITopicRepository topicRepository, IUserRepository userRepository) : IChatService
{
    private readonly ITopicRepository _topicRepository = topicRepository;
    private readonly IMessageRepository _messageRepository = messageRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task AddToChatAsync(string chatName, ITopicSubscriber subscriber)
    {
        var topic = await _topicRepository.GetOrCreateTopicAsync(topicName: chatName);
        await topic.AddSubscriberAsync(subscriber);
    }

    public async Task<IEnumerable<MessageAggregate>> GetMessagesAsync(string chatName, int? limit = null, int? offset = null)
    {
        var messages = await _messageRepository.GetMessagesByTopicAsync(chatName, limit ?? 128, offset ?? 64);
        var users = messages
            .Select(x => x.AuthorId)
            .Distinct()
            .ToArray();

        var userEntities = await _userRepository.GetUsersByIdsAsync(users!);
        var usersMap = userEntities.ToDictionary(usr => usr.Id);

        return messages.Select(x => new MessageAggregate(x, usersMap[x.AuthorId!]));
    }

    public async Task<bool> ReceiveMessageAsync(SendMessageDto messageDto)
    {
        var chatInfo = messageDto.ChatInfo;
        var topic = _topicRepository.GetTopic(chatInfo.ChatName);
        if (topic is null)
            return false;

        Message message = new()
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
