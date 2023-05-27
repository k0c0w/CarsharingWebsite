using System.ComponentModel;
using Carsharing.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carsharing.ViewModels;

public class EditUserVm
{
    private const string invalidSymbols = @"^[^$&+,:;=?@#|<>. -^*)(%!\""/№_}\[\]{{~]*$";
    private const string phoneRegEx = @"[0-9]{10}";

    
    [MaxLength(64, ErrorMessage = "К сожалению, новая фамилия может иметь не более 64 символов.")]
    [RegExCheck($"^[^{invalidSymbols}]*$", ErrorMessage = "К сожалению, фамилия сожержит недопустимые символы.")]
    [JsonPropertyName("LastName")]
    public string LastName { get; set; } 

    
    [MaxLength(64, ErrorMessage = "К сожалению, новое имя может иметь не более 64 символов.")]
    [RegExCheck($"^[^{invalidSymbols}]*$", ErrorMessage = "К сожалению, имя сожержит недопустимые символы.")]
    [JsonPropertyName("FirstName")]
    public string FirstName { get; set; }

    
    [EmailAddress(ErrorMessage ="В почте должен содержаться спецсимвол @")]
    [JsonPropertyName("Email")]
    public string Email { get; set; }

    [MaxLength(10,ErrorMessage ="Длина телефона  должна быть строго 10 цифр")]
    [RegExCheck($"{phoneRegEx}",ErrorMessage ="Телефон должен быть прямой последовательностью из 10 цифр")]
    [JsonPropertyName("Phone")]
    public string PhoneNumber { get; set; }

    [ValidateAge(AgeThreshold = 23, ErrorMessage = "Вам должно быть не менее 23 лет.")]
    [JsonPropertyName("Birthday")]
    public DateTime BirthDay { get; set; }
    [JsonPropertyName("PassportType")]
    public string PassportType { get; set; }
    [JsonPropertyName("Passport")]
    [RegExCheck($"{phoneRegEx}",ErrorMessage ="Пасспорт должен быть прямой последовательностью из 10 цифр")]
    public string Passport { get; set; }
    [JsonPropertyName("DriverLicense")]
    public int DriverLicense { get; set; } 
}