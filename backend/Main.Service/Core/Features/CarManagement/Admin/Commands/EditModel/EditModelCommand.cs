using Contracts;
using CQRS;

namespace Features.CarManagement.Admin;

public record EditModelCommand : ICommand 
{
    public int ModelId { get; init; }
    public IFile? Image { get; init; }
    public string? Description { get; init; } 
}
