using System.Text.Json.Serialization;

namespace Carsharing.ViewModels.Profile;

public record PersonalInfoVM
{
    [JsonPropertyName("email")]
    public string? Email { get; init; }
    
    [JsonPropertyName("name")]
    public string? FirstName { get; init; }
    
    [JsonPropertyName("surname")]
    public string? Surname { get; init; }
    
    [JsonPropertyName("phone")]
    public string? Phone { get; init; }
    
    [JsonPropertyName("passport")]
    public string? Passport { get; init; }
    
    [JsonPropertyName("driver_license")]
    public int? DriverLicense { get; init; }
    
    [JsonPropertyName("birthdate")]
    public DateOnly BirthDate { get; init; }
}