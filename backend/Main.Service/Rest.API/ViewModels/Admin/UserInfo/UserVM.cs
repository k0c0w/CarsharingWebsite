using System.Text.Json.Serialization;

namespace Carsharing.ViewModels.Admin.UserInfo;

public record UserVM
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }
    
    [JsonPropertyName("surname")]
    public string? LastName { get; init; }
    
    [JsonPropertyName("name")]
    public string? FirstName { get; init; }
    
    [JsonPropertyName("email")]
    public string? Email { get; init; }
    
    [JsonPropertyName("is_email_confirmed")]
    public bool EmailConfirmed { get; init; }
    
    [JsonPropertyName("personal_info")]
    public UserInfoVM? PersonalInfo { get; init; }
}