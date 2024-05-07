namespace Domain.Repositories;

public interface ITopicRepository
{
    Topic? GetTopic(string topicName);

    Task<Topic> GetOrCreateTopicAsync(string topicName);
    
    Task RemoveTopicAsync(string topicName);
}
