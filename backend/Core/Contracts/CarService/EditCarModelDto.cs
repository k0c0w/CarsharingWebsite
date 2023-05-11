namespace Contracts;

public record EditCarModelDto
{
    public string Brand { get; init; }
    
    public string Model { get; init; }

    public string Description { get; init; }
}