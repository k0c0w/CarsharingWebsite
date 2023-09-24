using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class User : IdentityUser
{
    [Required]
    public string? LastName { get; set; }
    
    [Required]
    public string? FirstName { get; set; }

    [AllowNull]
    public virtual UserInfo UserInfo { get; set; } = null;

    [InverseProperty("User")]
    public virtual ICollection<Subscription>? Subscriptions { get; } 
}
