using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GraphQL.API.Helpers.Attributes;

namespace GraphQL.API.Dtos;


public class LoginDto
{
    [RegExCheck(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Wrong email format")]
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [Required]
    [JsonPropertyName("password")]
    public string? Password { get; set; }
}