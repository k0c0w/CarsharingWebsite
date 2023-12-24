namespace Persistence.Chat.ChatEntites.SignalRModels;

public record ChatRoomUpdate
{
    public string RoomId { get; init; } = string.Empty;

    public string? RoomName { get; init;}

    public RoomUpdateEvent Event { get; init; }
}

public record OccasionChatRoomUpdate(Guid OccasionId) : ChatRoomUpdate;

public enum RoomUpdateEvent
{
    Created = 1,
    Deleted = 2,
    ManagerJoined = 3,
    ManagerLeft = 4,
}
