using Shared.CQRS;

namespace Features.CarManagement.Admin;

public record DeleteCarCommand(int Id) : ICommand;