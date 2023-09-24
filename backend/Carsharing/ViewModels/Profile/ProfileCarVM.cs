using System.Text.Json.Serialization;

namespace Carsharing.ViewModels.Profile;

public record ProfileCarVM
{
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; init; }
    
    [JsonPropertyName("model")]
    public string? Name { get; init; }
    
    [JsonPropertyName("license_plate")]
    public string? LicensePlate { get; init; }
    
    [JsonPropertyName("is_opened")]
    public bool IsOpened { get; init; }
}