namespace Persistence.Chat.ChatEntites.Dtos;

public record ChatMessageDto
{
    public string Text { get; init; } = string.Empty;

    public string AuthorId { get; init; } = string.Empty;

    public bool IsAuthorManager { get; init; }

    public DateTime Time { get; init; }

    public string RoomInitializerId { get; init; } = string.Empty;
}
