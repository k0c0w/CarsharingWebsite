using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contracts;

public class UserDto
{
    public string Id { get; set; }

    [JsonPropertyName("user_name")]
    public string UserName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

}