namespace Domain.Entities;

public class Message
{
    public Guid Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public DateTime Time { get; set; }

    public string AuthorId { get; set; } = string.Empty;

    public string TopicAuthorId { get; set; } = string.Empty;
}
