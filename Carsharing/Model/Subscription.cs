using System;
using System.Collections.Generic;

namespace Carsharing.Model;

public partial class Subscription
{
    public int Id { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateOnly? StartDate { get; set; }

    public bool? IsActive { get; set; }

    public int? CarId { get; set; }

    public int? ClientId { get; set; }

    public virtual CarPark? Car { get; set; }

    public virtual Client? Client { get; set; }
}
