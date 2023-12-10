using Domain.Entities;

namespace Entities.Entities;

public class OccasionMessage : Message
{
    public Guid OccasionId { get; set; }
    
    public Guid? Attachment { get; set; }
}