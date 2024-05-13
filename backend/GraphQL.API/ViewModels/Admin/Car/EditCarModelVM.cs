using Carsharing.Helpers.CustomValidators;

namespace GraphQL.API.ViewModels.Admin.Car;

public record EditCarModelVM
{
 
    public string? Description { get; init; }
    [ImageValidator(5000)]
    public IFormFile? Image { get; init; }
}