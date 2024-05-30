using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels;

public record CarVM
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(0)]
    public int Id { get; set; }

    [JsonPropertyName("license_plate")]
    [JsonPropertyOrder(2)]
    public string? LicensePlate { get; set; }

    [JsonPropertyName("latitude")]
    [JsonPropertyOrder(3)]
    public decimal ParkingLatitude { get; set; }
    
    [JsonPropertyName("longitude")]
    [JsonPropertyOrder(4)]
    public decimal ParkingLongitude { get; set; }
    
    [JsonPropertyName("description")]
    [JsonPropertyOrder(5)]
    public string? Description { get; set; }
    
    [JsonPropertyName("image_url")]
    [JsonPropertyOrder(6)]
    public string? ImageUrl { get; init; }
}