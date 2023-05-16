using Carsharing.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Carsharing.Forms;

public class RegistrationVm : LoginVM
{
    private const string invalidSymbols = @"^[^$&+,:;=?@#|<>.-^*)(%!\""/№_}\[\]{{~]*$";
    
    [Required(ErrorMessage = "Пожалуйста, укажите Ваше имя.")]
    [MaxLength(64, ErrorMessage = "К сожалению, имя может иметь не более 64 символов.")]
    [RegExCheck($"^[^{invalidSymbols}]*$", ErrorMessage = "К сожалению, имя сожержит недопустимые символы.")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Пожалуйста, укажите Вашу фамилию.")]
    [MaxLength(64, ErrorMessage = "К сожалению, фамилия может иметь не более 64 символов.")]
    [RegExCheck($"^[^{invalidSymbols}]*$", ErrorMessage = "К сожалению, фамилия сожержит недопустимые символы.")]
    [JsonPropertyName("surname")]
    public string Surname { get; set; }

    
    [Required(ErrorMessage = "Укажите Вашу дату рождения.")]
    [ValidateAge(AgeThreshold = 23, ErrorMessage = "Вам должно быть не менее 23 лет.")]
    [JsonPropertyName("birthday")]
    public DateTime Birthday { get; set; }
}
