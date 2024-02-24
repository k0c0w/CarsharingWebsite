using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Persistence.Chat.ChatEntites.SignalRModels;

public class TechSupportChatRoom : ChatRoom
{
    public string RoomId => Client.UserId;

    public TechSupportChatRoom(ChatUser client): base(client)
    {
    }
}
