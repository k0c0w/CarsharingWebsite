namespace Contracts;

public record CreateCarModelDto
{
    public string? Brand { get; init; }

    public string? Model { get; init; }

    public string? Description { get; init; }

    public int TariffId { get; init; }
    
    public Input.File? ModelPhoto { get; init; }
}