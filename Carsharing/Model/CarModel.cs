using System;
using System.Collections.Generic;

namespace Carsharing.Model;

public partial class CarModel
{
    public int Id { get; set; }

    public int? TarrifId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string SourceImg { get; set; } = null!;

    public virtual ICollection<CarPark> CarParks { get; } = new List<CarPark>();

    public virtual Tarrif? Tarrif { get; set; }
}
