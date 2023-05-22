using Carsharing.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carsharing.Forms;


public class LoginVM
{
    protected const string required = "Поле обязательно.";
    [Required(ErrorMessage = required)]
    [EmailAddress(ErrorMessage = "Не верный формат почты")]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [Required(ErrorMessage = required)]
    [MinLength(6, ErrorMessage = "Используйте пароль длиной в 6 символов и более.")]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}