using Shared.CQRS;

namespace Features.CarManagement.Admin;

public sealed record SetCarHasToBeNonActiveCommand(int Id) : ICommand;