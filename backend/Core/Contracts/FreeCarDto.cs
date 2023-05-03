namespace Contracts;

public class FreeCarDto
{
    public GeoPoint Location { get; init; }
    
    public int CarId { get; init; }
    
    public int TariffId { get; init; }
}