using Carsharing.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carsharing.Forms;


public class LoginDto
{
    [RegExCheck(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Wrong email format")]
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [Required]
    [JsonPropertyName("password")]
    public string? Password { get; set; }
}