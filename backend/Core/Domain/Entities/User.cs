﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;

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

    public virtual ICollection<UserUserRole> UserRoles { get; set; } = new List<UserUserRole>();
}
