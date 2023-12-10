using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Persistence.Chat.ChatEntites.SignalRModels;

public class OccasionsSupportChatRoom : ChatRoom
{
    public Guid Occasion { get; set; }
    public string RoomId { get; private set; }
    
    public OccasionsSupportChatRoom(OccasionChatUser client, string roomId, Guid occasion) : base(client)
    {
        RoomId = roomId;
        Occasion = occasion;
    }
}