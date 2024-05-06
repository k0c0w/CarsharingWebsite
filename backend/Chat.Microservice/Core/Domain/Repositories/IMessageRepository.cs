namespace Domain.Repositories;

public interface IMessageRepository
{

    public Task<IEnumerable<Message>> GetMessagesByTopicAsync(string topic, int limit = 100, int offset = 0);
}
