using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model;

public class Tariff
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    
    public int? MaxMileage { get; set; }
    
    public bool IsActive { get; set; }
}
