using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Persistence.Chat.ChatEntites.SignalRModels;

public class OccasionsSupportChatRoom : ChatRoom
{
    public Guid Occasion { get; set; }
    public Guid RoomId { get; private set; }
    
    public OccasionsSupportChatRoom(OccasionChatUser client, Guid roomId, Guid occasion) : base(client)
    {
        RoomId = roomId;
        Occasion = occasion;
    }
}