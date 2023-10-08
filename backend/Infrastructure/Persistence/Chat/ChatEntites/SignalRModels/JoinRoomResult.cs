namespace Persistence.Chat.ChatEntites.SignalRModels;

public record JoinRoomResult
{
    public bool Success { get; init; }

    public string RoomId { get; init; } = string.Empty;
}