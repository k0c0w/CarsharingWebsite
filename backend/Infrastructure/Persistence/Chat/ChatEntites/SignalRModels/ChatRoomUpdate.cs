namespace Persistence.Chat.ChatEntites.SignalRModels;

public record ChatRoomUpdate
{
    public string RoomId { get; init; } = string.Empty;

    public string? RoomName { get; init;}

    public RoomUpdateEvent Event { get; init; }
}

public enum RoomUpdateEvent
{
    Created,
    Deleted,
    ManagerJoined,
    ManagerLeft,
}
