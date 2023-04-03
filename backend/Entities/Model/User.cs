using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Entities.Model;

public class User : IdentityUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(Role))]
    public Roles RoleId { get; set; }

    public virtual UserInfo? UserInfo { get; set; }

    public virtual UserRole Role { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
