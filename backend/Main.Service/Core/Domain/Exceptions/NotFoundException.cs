namespace Entities.Exceptions;

public class NotFoundException : Exception
{
    public Type? ExpectedEntityType { get; }

    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string message, Type entity) : base(message)
    {
        ExpectedEntityType = entity;
    }
}
