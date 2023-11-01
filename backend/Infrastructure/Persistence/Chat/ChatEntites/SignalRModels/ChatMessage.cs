namespace Persistence.Chat.ChatEntites.SignalRModels;

public class ChatMessage
{
    public string? MessageId { get; set; }

    public string Text { get; set; } = string.Empty;

    public string AuthorName { get; set; } = string.Empty;

    public bool IsFromManager { get; set; }

    public DateTime Time { get; set; }

    public string RoomId { get; set; } = string.Empty;
}
