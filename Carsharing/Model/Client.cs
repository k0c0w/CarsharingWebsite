using System;
using System.Collections.Generic;

namespace Carsharing.Model;

public partial class Client
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ClientInfo? ClientInfo { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
