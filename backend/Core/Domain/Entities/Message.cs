namespace Domain.Entities;

public class Message
{
    public Message()
    {
        Id = Guid.NewGuid();
    }

    public Message(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Empty value", nameof(id));

        Id = id;
    }


    public Guid Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public DateTime Time { get; set; }

    public string AuthorId { get; set; } = string.Empty;

    public string TopicAuthorId { get; set; } = string.Empty;

    public bool IsFromManager { get; set; }
}
