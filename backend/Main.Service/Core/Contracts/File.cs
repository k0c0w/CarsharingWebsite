namespace Contracts.Input;

public record File : IFile
{
    public string? Name { get; init; }

    public Stream? Content { get; init; }

    public string ContentType { get; init; } = string.Empty;
}