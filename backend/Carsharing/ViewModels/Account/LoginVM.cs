using Carsharing.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carsharing.Forms;


public class LoginVM
{
    [Required(ErrorMessage = "Поле 'почта' обязательно.")]
    [RegExCheck(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Не верный формат почты")]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Поле 'пароль' обязательно.")]
    [MinLength(6, ErrorMessage = "Используйте пароль длиной в 6 символов и более.")]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}