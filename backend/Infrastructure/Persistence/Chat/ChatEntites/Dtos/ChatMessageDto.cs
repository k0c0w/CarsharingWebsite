namespace Persistence.Chat.ChatEntites.Dtos;

public record ChatMessageDto
{
    public string Text { get; set; } = string.Empty;

    public string AuthorId { get; set; } = string.Empty;

    public DateTime Time { get; set; }

    public string RoomInitializerId { get; set; } = string.Empty;
}
