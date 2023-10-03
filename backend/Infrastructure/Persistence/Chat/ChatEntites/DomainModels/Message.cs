namespace Persistence.Chat.ChatEntites.DomainModels;

public class Message
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public DateTime Time { get; set; }

    public string AuthorId { get; set; }

    public string TopicAuthorId { get; set; }
}
