namespace Contracts;

public record File : IFile
{
    public string Name { get; init; }
    
     public Stream Content { get; init; }
}