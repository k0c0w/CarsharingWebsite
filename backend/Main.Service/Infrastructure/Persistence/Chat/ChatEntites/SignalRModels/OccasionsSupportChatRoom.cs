using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Persistence.Chat.ChatEntites.SignalRModels;

public class OccasionChatRoom
{
    public Guid IssuerId { get; set; }

    public Guid Occasion { get; set; }
    public Guid RoomId { get; private set; }
    
    public OccasionChatRoom(Guid issuerId, Guid roomId, Guid occasion)
    {
        RoomId = roomId;
        Occasion = occasion;
        IssuerId = issuerId;
    }
}