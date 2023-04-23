using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities.Model;

public class Car
{
    //todo: [concurencyCheck]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(9)]
    public string LicensePlate { get; set; }

    //todo: [concurencyCheck]
    public bool HasToBeNonActive { get; set; }

    public bool IsOpened { get; set; }

    //todo: [concurencyCheck]
    public bool IsTaken { get; set; }

    [ForeignKey(nameof(CarModelId))]
    public int CarModelId { get; set; }

    public virtual CarModel CarModel { get; set; }
}
