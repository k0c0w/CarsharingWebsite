using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels;

public record ExpandedCarModelVM : CarModelVM
{
    [JsonPropertyName("tariff_name")]
    [JsonPropertyOrder(7)]
    public string? TariffName { get; init; }
    
    [JsonPropertyName("price")]
    [JsonPropertyOrder(8)]
    public decimal Price { get; init; }
    
    [JsonPropertyName("max_milage")]
    [JsonPropertyOrder(9)]
    public int? MaxMilage { get; init; }
}