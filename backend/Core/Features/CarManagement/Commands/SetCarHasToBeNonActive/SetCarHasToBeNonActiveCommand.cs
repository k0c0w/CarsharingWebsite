using Shared.CQRS;

namespace Features.CarManagement.Commands.SetCarHasToBeNonActive;

public sealed record SetCarHasToBeNonActiveCommand(int Id) : ICommand;