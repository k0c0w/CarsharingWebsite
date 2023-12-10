namespace Persistence.Chat.ChatEntites.SignalRModels.Shared;

public class OccasionChatMessage
{
    public string? MessageId { get; set; }

    public string Text { get; set; } = string.Empty;

    public string AuthorName { get; set; } = string.Empty;

    public bool IsFromManager { get; set; }

    public DateTime Time { get; set; }

    public Guid RoomId { get; set; } 

    public Guid? Attachment { get; set; }
}