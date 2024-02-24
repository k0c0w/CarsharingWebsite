using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Tariff
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TariffId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }
    
    public int? MaxMileage { get; set; }
    
    public bool IsActive { get; set; }

    public string ImageUrl { get; set; } = null!;
}
