using System.ComponentModel.DataAnnotations;

namespace GraphQL.API.ViewModels.Admin.Car;

public record CreateCarModelVM
{
    [Required(ErrorMessage = "Укажите марку автомобиля.")]
    public string? Brand { get; init; }

    [Required(ErrorMessage = "Укажите модель автомобиля.")]
    public string? Model { get; init; }

    [Required(ErrorMessage = "Укажите тариф, к которому привязан прототип.")]
    [Range(1, Int32.MaxValue, ErrorMessage = "Не верный id")]
    public int TariffId { get; init; }
    
    [Required(ErrorMessage = "Укажите описание прототипа.")]
    public string? Description { get; init; }

    //[ImageValidator(10000000)]
    public IFormFile? Image { get; init; }
}