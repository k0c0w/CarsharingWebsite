using Carsharing.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Carsharing.Forms;

public class RegistrationDto : LoginDto
{

    /*[Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
    [JsonPropertyName()]
    public string RetryPassword { get; set; } = String.Empty;*/

    [Required]
    [RegExCheck(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Wrong format for name")]
    [JsonPropertyName("name")]
    public string UserName { get; set; } = String.Empty;

    [Required]
    [RegExCheck(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "Wrong format for name")]
    [JsonPropertyName("surname")]
    public string UserSurname { get; set; } = String.Empty;


    [DataType(DataType.DateTime)]
    public DateTime Birthday { get; set; }
}
