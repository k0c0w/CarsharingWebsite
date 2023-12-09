namespace Contracts.Occasion;

public class OccasionStatusChangeDto
{
    public Guid OccasionId { get; set; }

    public OccasionStatusChange ChangeType { get; set; }
}

public enum OccasionStatusChange
{
    Created,
    Completed
}