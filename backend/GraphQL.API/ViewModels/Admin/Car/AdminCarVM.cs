
using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels.Admin.Car;

public record AdminCarVM : CarVM
{
    [JsonPropertyName("prototype_id")]
    [JsonPropertyOrder(1)]
    public int CarModelId { get; set; }
    
    [JsonPropertyName("has_to_be_non_active")]
    [JsonPropertyOrder(7)]
    public bool HasToBeNonActive { get; set; }

    [JsonPropertyName("is_opened")]
    [JsonPropertyOrder(6)]
    public bool IsOpened { get; set; }

    [JsonPropertyName("is_taken")]
    [JsonPropertyOrder(5)]
    public bool IsTaken { get; set; }
}