using Domain.Entities;

namespace Entities.Entities;

public class OccasionMessage
{
    public Guid OccasionId { get; set; }
    
    public Guid? Attachment { get; set; }
    
    public Guid Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public DateTime Time { get; set; }

    public string AuthorId { get; set; } = string.Empty;

    public string TopicAuthorId { get; set; } = string.Empty;

    public bool IsFromManager { get; set; }
}