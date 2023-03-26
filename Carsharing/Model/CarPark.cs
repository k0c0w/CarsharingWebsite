using System;
using System.Collections.Generic;

namespace Carsharing.Model;

public partial class CarPark
{
    public int Id { get; set; }

    public int? CarModelId { get; set; }

    public bool? IsOpened { get; set; }

    public int GovermentNumber { get; set; }

    public bool? IsTaken { get; set; }

    public virtual CarModel? CarModel { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
