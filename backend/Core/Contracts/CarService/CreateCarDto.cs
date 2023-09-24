namespace Contracts;

public class CreateCarDto
{
    public string? LicensePlate { get; set; }
    
    public decimal ParkingLatitude { get; set; }
    
    public decimal ParkingLongitude { get; set; }

    public int CarModelId { get; set; }
}