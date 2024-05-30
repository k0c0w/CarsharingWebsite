using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class CarModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    [ForeignKey(nameof(Tariff))]
    public int TariffId { get; set; }
    
    public virtual Tariff? Tariff { get; set; }
}
