using System.ComponentModel.DataAnnotations;

namespace Carsharing.ViewModels.Admin;

public class CreateTariffVM
{
    [Required]
    [MaxLength(64, ErrorMessage = "Не более 64 символов")]
    public string Name { get; set; }
    
    [Required]
    [Range(0, Double.MaxValue, ErrorMessage = "Не может быть отрицательным")]
    public decimal Price { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Не может быть отрицательным")]
    public int? MaxMillage { get; init; }
    
    [Required]
    [MinLength(1)]
    public string Description { get; set; }
}