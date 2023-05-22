using System.Text.Json.Serialization;

namespace Carsharing.ViewModels.Admin;

public class TariffVM
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(0)]
    public int Id { get; init; }
    
    [JsonPropertyName("name")]
    [JsonPropertyOrder(1)]
    public string Name { get; init; }
    
    [JsonPropertyName("description")]
    [JsonPropertyOrder(2)]
    public string Description { get; init; }
    
    [JsonPropertyName("is_active")]
    [JsonPropertyOrder(3)]
    public bool IsActive { get; init; }
    
    [JsonPropertyName("max_mileage")]
    [JsonPropertyOrder(4)]
    public int? MaxMileage { get; init; }
    
    [JsonPropertyName("price")]
    [JsonPropertyOrder(5)]
    public decimal PriceInRubles { get; init; }
}