using Carsharing.Helpers.CustomValidators;

namespace Carsharing.ViewModels.Admin.Car;

public record EditCarModelVM
{
    //todo:
    public string? Description { get; init; }
    
    public IFormFile? Image { get; init; }
}