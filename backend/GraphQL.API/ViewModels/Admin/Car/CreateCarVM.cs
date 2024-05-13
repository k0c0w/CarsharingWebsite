using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels.Admin.Car;

public record CreateCarVM
{
    [JsonPropertyName("license_plate")]
    public string? LicensePlate { get; init; }

    [JsonPropertyName("latitude")]
    public decimal ParkingLatitude { get; init; }
    
    [JsonPropertyName("longitude")]
    public decimal ParkingLongitude { get; init; }
    
    [JsonPropertyName("prototype_id")]
    public int CarModelId { get; init; }
}