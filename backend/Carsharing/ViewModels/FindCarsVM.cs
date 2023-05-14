using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carsharing.ViewModels;

public record FindCarsVM
{
    [Required(ErrorMessage = "Укажите  id прототипа.")]
    [JsonPropertyName("car_model_id")]
    public int CarModelId { get; init; }
    
    [Required(ErrorMessage = "Укажите долготу.")]
    [JsonPropertyName("longitude")]
    public decimal Longitude { get; init; }
    
    [Required(ErrorMessage = "Укажите широту.")]
    [JsonPropertyName("latitude")]
    public decimal Latitude { get; init; }
    
    [Required(ErrorMessage = "Укажите радиус поиска.")]
    [Range(20, 100000)]
    [JsonPropertyName("radius")]
    public int Radius { get; init; }
}