using Shared.CQRS;

namespace Features.CarManagement.Commands.SetCarTaken;

public sealed record SetCarTakenCommand(int Id) : ICommand;