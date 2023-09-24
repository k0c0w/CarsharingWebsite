using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carsharing.ViewModels.Admin;

public class CreateTariffVM
{
    [Required]
    [MaxLength(64, ErrorMessage = "Не более 64 символов")]
    public string? Name { get; set; }
    
    [Required]
    [Range(0, Double.MaxValue, ErrorMessage = "Не может быть отрицательным")]
    public decimal Price { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Не может быть отрицательным")]
    [JsonPropertyName("max_millage")]
    public int? MaxMillage { get; init; }
    
    [Required]
    public string? Description { get; set; }
}