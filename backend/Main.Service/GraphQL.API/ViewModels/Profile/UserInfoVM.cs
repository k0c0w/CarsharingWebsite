using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels.Profile;

public class UserInfoVM
{
    [JsonPropertyName("email")]
    public string? Email { get; init; }
    
    [JsonPropertyName("full_name")]
    public string? FullName { get; init; }
    
    [JsonPropertyName("balance")]
    public decimal Balance { get; init; }

    [JsonPropertyName("first_name")]
    public string? Name { get; init; }

    [JsonPropertyName("second_name")]
    public string? SecondName { get; init; }

    [JsonPropertyName("birthdate")]
    public DateTime? BirthDate { get; init; }

    [JsonPropertyName("confirmed")]
    public bool IsConfirmed { get; init; }
}