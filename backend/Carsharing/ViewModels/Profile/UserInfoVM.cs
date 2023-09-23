using System.Text.Json.Serialization;

namespace Carsharing.ViewModels.Profile;

public record UserInfoVM
{
    [JsonPropertyName("email")]
    public string? Email { get; init; }
    
    [JsonPropertyName("full_name")]
    public string? FullName { get; init; }
    
    [JsonPropertyName("balance")]
    public decimal Balance { get; init; }
}