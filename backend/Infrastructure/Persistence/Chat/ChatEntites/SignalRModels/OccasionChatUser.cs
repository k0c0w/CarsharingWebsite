using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Persistence.Chat.ChatEntites.SignalRModels;

public class OccasionChatUser : ChatUserBase
{
    public Guid JoinedRoomId { get; set; }

    public OccasionChatUser(string userId, string? userName) : base(userId, userName)
    {
    }
}