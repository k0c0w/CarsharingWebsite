namespace Contracts;

public record SearchCarDto
{
    public int CarModelId { get; init; }
    
    public double Longitude { get; init; }
    
    public double Latitude { get; init; }
    
    public int Radius { get; init; }
}