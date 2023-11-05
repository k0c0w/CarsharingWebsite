using Shared.CQRS;

namespace Features.CarManagement.Commands.DeleteCar;

public record DeleteCarCommand(int Id) : ICommand;