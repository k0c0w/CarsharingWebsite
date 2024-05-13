using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GraphQL.API.Helpers.Attributes;

namespace GraphQL.API.ViewModels.Account;

public class RegistrationVm : LoginVM
{
    public RegistrationVm(string? accept, DateTime birthdate, string name, string surname, string password, string email) 
    : base(password, email)
    {
        Accept = accept;
        Surname = surname;
        Name = name;
        Birthdate = birthdate;
    }

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
    
    [Required(ErrorMessage = "Необходимо Ваше согласие на обработку персональных данных.")]
    [Compare("AcceptMustBe", ErrorMessage = "Необходимо Ваше согласие на обработку персональных данных.")]
    [JsonPropertyName("accept")]
    public string? Accept { get; set; }
}
