using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Entities.Model;

public class User : IdentityUser
{

    //public virtual int UserInfoId { get; set; }
    [AllowNull]
    public virtual UserInfo UserInfo { get; set; } = null;

    [InverseProperty("User")]
    public virtual ICollection<Subscription>? Subscriptions { get; } 
}
