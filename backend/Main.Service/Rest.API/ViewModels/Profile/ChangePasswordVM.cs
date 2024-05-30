using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Carsharing.Helpers.Attributes;

namespace Carsharing.ViewModels.Profile;

public record ChangePasswordVM
{
    private const string required = "Поле обязательно.";
    private const string sixSymbols = "Не менее 6 символов.";
    
    [Required(ErrorMessage = required)]
    [MinLength(6, ErrorMessage = sixSymbols)]
    [JsonPropertyName("old_password")]
    public string? OldPassword { get; init; }
    
    [Required(ErrorMessage = required)]
    [MinLength(6, ErrorMessage = sixSymbols)]
    [FieldsAreNotEqual(OtherProperty = "OldPassword", ErrorMessage = "Пароль совпадает со старым.")]
    [JsonPropertyName("password")]
    public string? Password { get; init; }
}