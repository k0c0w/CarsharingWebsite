using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Persistence;

public interface IChatUserRepository<TChatUser> where TChatUser : ChatUserBase
{
    public bool TryGetUser(string userId, out TChatUser? chatUser);

    public bool TryRemoveUser(string userId, out TChatUser? chatUser);

    public bool TryAddUser(string userId, TChatUser user);

    public bool ContainsUserById(string userId);
}
