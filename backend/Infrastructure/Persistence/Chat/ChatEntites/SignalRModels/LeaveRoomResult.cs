namespace Persistence.Chat.ChatEntites.SignalRModels;

public record LeaveRoomResult
{
    public bool Success { get; init; }

    public string RoomId { get; init; } = string.Empty;
}