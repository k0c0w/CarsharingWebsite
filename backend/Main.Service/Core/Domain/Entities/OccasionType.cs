namespace Entities.Entities;

public class OccasionType
{
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;
}

public enum OccasionTypeDefinition
{
    RoadAccident = 1,
    VehicleBreakdown=2,
    Other=2048
}