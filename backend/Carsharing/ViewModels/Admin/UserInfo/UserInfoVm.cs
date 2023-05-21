using Domain.Entities;

namespace Carsharing.ViewModels.Admin.UserInfo;

public class UserInfoVm
{
    public int UserInfoId { get; set; }
    public DateTime BirthDay { get; set; }
    public string? PassportType { get; set; }
    public string? Passport { get; set; }
    public int? DriverLicense { get; set; }
    public decimal Balance { get; set; }
    public string? UserId { get; set; }
    public bool? Verified { get; set; }
    public User User { get; set; }
}