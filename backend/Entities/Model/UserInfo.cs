using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Entities.Model;

public class UserInfo
{
    [Key]
    public int UserInfoId { get; set; }
    public DateTime BirthDay { get; set; }
    public string? PassportType { get; set; }
    public string? Passport { get; set; }
    public int? DriverLicense { get; set; }
    public decimal Balance { get; set; }


    public string UserId { get; set; } 

    [AllowNull]
    public virtual User User { get; set; } = null;
}
