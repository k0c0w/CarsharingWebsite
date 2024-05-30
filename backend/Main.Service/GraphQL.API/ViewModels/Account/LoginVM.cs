using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels.Account;

public class LoginVM
{
    protected const string required = "Поле обязательно.";
    [Required(ErrorMessage = required)]
    [EmailAddress(ErrorMessage = "Не верный формат почты")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = required)]
    [MinLength(6, ErrorMessage = "Используйте пароль длиной в 6 символов и более.")]
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    public LoginVM(string password, string email)
    {
        Password = password;
        Email = email;
    }
}