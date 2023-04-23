using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model;

public class CarModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Brand { get; set; }
    
    public string Model { get; set; }

    public string Description { get; set; }

    [ForeignKey(nameof(Tariff))]
    public int TariffId { get; set; }

    public virtual Tariff Tariff { get; set; }
}
