namespace Persistence.Chat.ChatEntites.Dtos;

public class OccasionChatMessageDto
{
    public string Text { get; init; } = string.Empty;

    public string AuthorId { get; init; } = string.Empty;
    

    public Guid? Attachment { get; init; } = default;

    public bool IsAuthorManager { get; init; }

    public DateTime Time { get; init; }

    public Guid OccasionId { get; init; }
}