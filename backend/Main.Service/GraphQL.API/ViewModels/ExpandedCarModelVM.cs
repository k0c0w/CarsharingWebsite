using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels;

public record ExpandedCarModelVM : CarModelVM
{
    public string? TariffName { get; init; }
    
    public decimal Price { get; init; }
    
    public int? MaxMilage { get; init; }
}