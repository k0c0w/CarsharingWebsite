namespace Domain.Repositories;

public interface IMessageRepository
{
    public Task<IEnumerable<MessageAggregate>> GetMessagesByTopicAsync(string topic, int limit = 100, int offset = 0);

    public Task AddAsync(Message message);
}
