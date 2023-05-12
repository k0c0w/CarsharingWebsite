using System.Text.Json.Serialization;

namespace Carsharing.ViewModels.Admin.Car;

public record CreateCarVM
{
    [JsonPropertyName("license_plate")]
    public string LicensePlate { get; init; }

    [JsonPropertyName("latitude")]
    public double ParkingLatitude { get; init; }
    
    [JsonPropertyName("longitude")]
    public double ParkingLongitude { get; init; }
    
    [JsonPropertyName("prototype_id")]
    public int CarModelId { get; init; }
}