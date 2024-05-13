using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels.Admin.UserInfo;

public class UserInfoVM
{
    [JsonPropertyName("birthdate")]
    public DateOnly BirthDay { get; set; }
    
    [JsonPropertyName("passport")]
    public string? Passport { get; set; }
    
    [JsonPropertyName("driver_license")]
    public int? DriverLicense { get; set; }
    
    [JsonPropertyName("account_balance")]
    [JsonPropertyOrder(1)]
    public decimal Balance { get; set; }
    
    [JsonPropertyName("is_info_verified")]
    [JsonPropertyOrder(0)]
    public bool? Verified { get; set; }
}