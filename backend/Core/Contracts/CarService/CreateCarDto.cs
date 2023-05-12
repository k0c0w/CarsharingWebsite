namespace Contracts;

public class CreateCarDto
{
    public string LicensePlate { get; set; }
    
    public double ParkingLatitude { get; set; }
    
    public double ParkingLongitude { get; set; }

    public int CarModelId { get; set; }
}