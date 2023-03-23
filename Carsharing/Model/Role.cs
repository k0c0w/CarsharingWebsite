using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carsharing.Model;

public partial class Role
{
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    public virtual Client? Client { get; set; }
}
