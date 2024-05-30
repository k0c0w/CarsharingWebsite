namespace Contracts;

public interface IFile
{
    string? Name { get; }
    
    Stream? Content { get; }
}