using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Persistence.Chat.ChatEntites.SignalRModels;

public class ChatUser : ChatUserBase
{
    public bool IsAnonymous { get; }

    public ChatUser(string userId, string? userName) : base(userId, userName)
    {
        IsAnonymous = userName == null;
    }
}
