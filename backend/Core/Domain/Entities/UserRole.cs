using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;


public class UserRole: IdentityRole
{
    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserUserRole> UserRoles { get; set; } = new List<UserUserRole>();
}
