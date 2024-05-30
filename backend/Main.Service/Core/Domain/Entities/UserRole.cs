using Entities.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;


public class UserRole: IdentityRole
{
    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserUserRole> UserRoles { get; set; } = new List<UserUserRole>();
}
