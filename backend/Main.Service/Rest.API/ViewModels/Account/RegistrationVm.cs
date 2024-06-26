using Carsharing.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Carsharing.Forms;

public class RegistrationVm
{
    private const string invalidSymbols = @"^[^$&+,:;=?@#|<>.-^*)(%!\""/№_}\[\]{{~]*$";
    
    [Required(ErrorMessage = "Пожалуйста, укажите Ваше имя.")]
    [MaxLength(64, ErrorMessage = "К сожалению, имя может иметь не более 64 символов.")]
    [RegExCheck($"^[^{invalidSymbols}]*$", ErrorMessage = "К сожалению, имя сожержит недопустимые символы.")]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Пожалуйста, укажите Вашу фамилию.")]
    [MaxLength(64, ErrorMessage = "К сожалению, фамилия может иметь не более 64 символов.")]
    [RegExCheck($"^[^{invalidSymbols}]*$", ErrorMessage = "К сожалению, фамилия сожержит недопустимые символы.")]
    [JsonPropertyName("surname")]
    public string? Surname { get; set; }
    
    [Required(ErrorMessage = "Пожалуйста, укажите Вашу дату рождения.")]
    [ValidateAge(AgeThreshold = 23, ErrorMessage = "Вам должно быть не менее 23 лет.")]
    [JsonPropertyName("birthdate")]
    public DateTime Birthdate { get; set; }

    protected const string required = "Поле обязательно.";
    [Required(ErrorMessage = required)]
    [EmailAddress(ErrorMessage = "Не верный формат почты")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = required)]
    [MinLength(6, ErrorMessage = "Используйте пароль длиной в 6 символов и более.")]
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Необходимо Ваше согласие на обработку персональных данных.")]
    [Compare("AcceptMustBe", ErrorMessage = "Необходимо Ваше согласие на обработку персональных данных.")]
    [JsonPropertyName("accept")]
    public string? Accept { get; set; }

    [ValidateNever] public string AcceptMustBe => "on";
}
