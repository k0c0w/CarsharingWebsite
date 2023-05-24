using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class CarModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Brand { get; set; } = String.Empty;

    public string Model { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    [ForeignKey(nameof(Tariff))]
    public int TariffId { get; set; }

    public virtual Tariff? Tariff { get; set; }

    public string ImageName => $"{Brand.Replace(' ', '_')}_{Model.Replace(' ', '_')}_{TariffId}.png";
}
