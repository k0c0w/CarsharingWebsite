namespace Contracts;

public record CarDto
{
    public int Id { get; set; }

    public string LicensePlate { get; set; }

    public bool HasToBeNonActive { get; set; }

    public bool IsOpened { get; set; }

    public bool IsTaken { get; set; }
    
    public decimal ParkingLatitude { get; set; }
    
    public decimal ParkingLongitude { get; set; }

    public int CarModelId { get; set; }
}