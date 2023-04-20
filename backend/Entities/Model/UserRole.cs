using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Entities.Model;


public class UserRole: IdentityRole
{
    public DateTime? CreatedAt { get; set; }
}
