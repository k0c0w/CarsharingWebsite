using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carsharing.ViewModels.Admin;

public class CreateTariffVM
{
    [Required(AllowEmptyStrings = false)]
    [MaxLength(64, ErrorMessage = "Не более 64 символов")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = "No content";
    
    [Required]
    [JsonPropertyName("price")]
    [Range(0, Double.MaxValue, ErrorMessage = "Не может быть отрицательным")]
    public decimal PricePerMinute { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Не может быть отрицательным")]
    [JsonPropertyName("max_millage")]
    public int? MaxMillage { get; init; }

    [JsonPropertyName("min_rent_minutes")]
    [Range(0, Double.MaxValue, ErrorMessage = "Не может быть отрицательным")]
    public long MinRentMinutes { get; init; }

    [JsonPropertyName("max_rent_minutes")]
    [Range(0, Double.MaxValue, ErrorMessage = "Не может быть отрицательным")]
    public long MaxRentMinutes { get; init; }

    [Required(AllowEmptyStrings = false)]
    [JsonPropertyName("description")]
    public string Description { get; set; } = "No content";
}