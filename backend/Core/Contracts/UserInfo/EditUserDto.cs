using System.ComponentModel.DataAnnotations;
namespace Contracts.UserInfo;

public class EditUserDto
{  
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime BirthDay { get; set; }
    public string? PassportType { get; set; }
    public string? Passport { get; set; }
    public int DriverLicense { get; set; }
}