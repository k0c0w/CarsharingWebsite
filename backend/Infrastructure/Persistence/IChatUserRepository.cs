using Persistence.Chat.ChatEntites.SignalRModels;
namespace Persistence;

public interface IChatUserRepository
{
    public bool TryGetUser(string userId, out ChatUser? chatUser);

    public bool TryRemoveUser(string userId, out ChatUser? chatUser);

    public bool TryAddUser(string userId, ChatUser user);

    public bool ContainsUserById(string userId);
}
