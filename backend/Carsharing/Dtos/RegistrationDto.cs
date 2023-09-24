using Carsharing.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carsharing.Forms;

public class RegistrationDto : LoginDto
{
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
