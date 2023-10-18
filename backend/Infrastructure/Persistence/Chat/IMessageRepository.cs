using Domain.Entities;
using Domain.Repository;
using Persistence.Chat.ChatEntites.SignalRModels;

namespace Persistence.Chat;

public interface IMessageRepository : IRepository<Message, Guid>
{

    public Task<IEnumerable<ChatMessage>> GetMessagesAssosiatedWithUserAsync(string userId, int offset, int limit);
}
