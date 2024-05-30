namespace Features.CarManagement.Admin;

public record CreateCarCommand : ICommand<int>
{
    public string? LicensePlate { get; init; }

    public decimal ParkingLatitude { get; init; }

    public decimal ParkingLongitude { get; init; }

    public int CarModelId { get; init; }
}
