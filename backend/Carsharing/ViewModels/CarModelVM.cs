using System.Text.Json.Serialization;

namespace Carsharing.ViewModels;

public record CarModelVM
{
    [JsonPropertyName("id")]
    [JsonPropertyOrder(0)]
    public int Id { get; set; }
    
    [JsonPropertyName("brand")]
    [JsonPropertyOrder(2)]
    public string? Brand { get; set; }
    
    [JsonPropertyName("model")]
    [JsonPropertyOrder(3)]
    public string? Model { get; set; }

    [JsonPropertyName("description")]
    [JsonPropertyOrder(4)]
    public string? Description { get; set; }
    
    [JsonPropertyName("tariff_id")]
    [JsonPropertyOrder(6)]
    public int TariffId { get; set; }
    
    [JsonPropertyName("image_url")]
    [JsonPropertyOrder(5)]
    public string? Url { get; set; }
}