using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities;

public class UserUserRole : IdentityUserRole<string>
{
    public virtual User User { get; set; } = new User();
    public virtual UserRole Role { get; set; } = new UserRole();
}
