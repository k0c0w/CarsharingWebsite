namespace Contracts;

public record EditCarModelDto
{
    public IFile? Image { get; init; }
    public string? Description { get; init; }
}