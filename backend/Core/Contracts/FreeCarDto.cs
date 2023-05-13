namespace Contracts;

public record FreeCarDto
{
    public GeoPoint Location { get; init; }
    
    public int CarId { get; init; }
    
    public int TariffId { get; init; }
}