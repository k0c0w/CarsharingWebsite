namespace Contracts;

public record SearchCarDto
{
    public int CarModelId { get; init; }
    
    public decimal Longitude { get; init; }
    
    public decimal Latitude { get; init; }
    
    public int Radius { get; init; }
}