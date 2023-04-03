using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities.Model;

public class Car
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(9)]
    public string LicensePlate { get; set; }

    public bool HasToBeNonActive { get; set; }

    public bool IsOpened { get; set; }

    public bool IsTaken { get; set; }

    [ForeignKey(nameof(CarModelId))]
    public int CarModelId { get; set; }

    public virtual CarModel CarModel { get; set; }
}
