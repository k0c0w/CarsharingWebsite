using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GraphQL.API.Helpers.Attributes;

namespace GraphQL.API.ViewModels;

public class EditUserVm
{
    private const string invalidSymbols = @"^[^$&+,:;=?@#|<>.-^*)(%!\""/№_}\[\]{{~]*$";


    [MaxLength(64, ErrorMessage = "К сожалению, новая фамилия может иметь не более 64 символов.")]
    [RegExCheck($"^[^{invalidSymbols}]*$", ErrorMessage = "К сожалению, фамилия сожержит недопустимые символы.")]
    [JsonPropertyName("surname")]
    public string? LastName { get; set; } 
    
    [MaxLength(64, ErrorMessage = "К сожалению, новое имя может иметь не более 64 символов.")]
    [RegExCheck($"^[^{invalidSymbols}]*$", ErrorMessage = "К сожалению, имя сожержит недопустимые символы.")]
    [JsonPropertyName("name")]
    public string? FirstName { get; set; }

    [EmailAddress(ErrorMessage ="В почте должен содержаться спецсимвол @")]
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    [ValidateAge(AgeThreshold = 23, ErrorMessage = "Вам должно быть не менее 23 лет.")]
    [JsonPropertyName("birthdate")]
    public DateTime BirthDay { get; set; }
    
    [JsonPropertyName("passport")]
    [RegExCheck(@"(^$)|(^\d{10}$)", ErrorMessage = "Пасспорт должен быть прямой последовательностью из 10 цифр")]
    public string? Passport { get; set; }
    
    [JsonPropertyName("license")]
    public int? DriverLicense { get; set; } 
}