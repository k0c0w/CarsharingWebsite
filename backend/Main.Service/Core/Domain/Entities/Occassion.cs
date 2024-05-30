namespace Entities.Entities;

public class Occassion
{
    public Guid Id { get; set; }

    public string IssuerId { get; set; } = string.Empty;

    public DateTime CreationDateUtc { get; set; }
    
    public DateTime? CloseDateUtc { get; set; }

    public OccasionTypeDefinition OccasionType { get; set; }

    public string Topic { get; set; } = string.Empty;
}
