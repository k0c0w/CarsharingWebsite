using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public enum Roles
{
    Admin=1,
    User
}

//todo: заменить Id на enum и мапить его в инт

[PrimaryKey("Id")]
public class UserRole
{
    public Roles Id { get; set; }

    public string Name { get; set; }
}
