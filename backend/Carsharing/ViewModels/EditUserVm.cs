using Carsharing.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carsharing.ViewModels;

public class EditUserVm
{
    public string UserSurname { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime BirthDay { get; set; } = DateTime.MinValue;
    public string PassportType { get; set; } = string.Empty;
    public string Passport { get; set; } = string.Empty;
    public int DriverLicense { get; set; } = 0000000000;
}