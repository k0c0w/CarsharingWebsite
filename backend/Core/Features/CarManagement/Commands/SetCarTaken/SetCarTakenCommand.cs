using Shared.CQRS;

namespace Features.CarManagement;

public sealed record SetCarTakenCommand(int Id) : ICommand;