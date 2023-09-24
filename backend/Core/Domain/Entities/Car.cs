using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Car
{
    [ConcurrencyCheck]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(9)]
    public string? LicensePlate { get; set; }

    [ConcurrencyCheck]
    public bool HasToBeNonActive { get; set; }

    public bool IsOpened { get; set; }

    [ConcurrencyCheck]
    public bool IsTaken { get; set; }
    
    public decimal ParkingLatitude { get; set; }
    
    public decimal ParkingLongitude { get; set; }

    [ForeignKey(nameof(CarModelId))]
    public int CarModelId { get; set; }

    public virtual CarModel? CarModel { get; set; }
}
