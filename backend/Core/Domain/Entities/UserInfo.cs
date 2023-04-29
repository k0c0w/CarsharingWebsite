using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey("UserId")]
public class UserInfo
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public DateTime BirthDay { get; set; }

    public string? PassportType { get; set; }

    public string? Passport { get; set; }

    public int? DriverLicense { get; set; }

    public string? TelephoneNum { get; set; }

    [ConcurrencyCheck]
    public decimal Balance { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public virtual User User { get; set; }
}
