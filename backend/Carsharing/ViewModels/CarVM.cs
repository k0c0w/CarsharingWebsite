using System.Text.Json.Serialization;

namespace Carsharing.ViewModels;

public record CarVM
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(0)]
    public int Id { get; set; }

    [JsonPropertyName("license_plate")]
    [JsonPropertyOrder(2)]
    public string LicensePlate { get; set; }

    [JsonPropertyName("latitude")]
    [JsonPropertyOrder(3)]
    public decimal ParkingLatitude { get; set; }
    
    [JsonPropertyName("longitude")]
    [JsonPropertyOrder(4)]
    public decimal ParkingLongitude { get; set; }
}