using Shared.CQRS;

namespace Features.CarManagement.Commands.TryDeleteModel;

public record TryDeleteModelCommand(int Id) : ICommand;