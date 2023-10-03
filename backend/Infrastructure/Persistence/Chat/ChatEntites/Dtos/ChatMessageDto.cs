namespace Persistence.Chat.ChatEntites.Dtos;

public record ChatMessageDto
{
    public string Text { get; set; }

    public string AuthorId { get; set; }

    public DateTime Time { get; set; }

    public string RoomInitializerId { get; set; }
}
