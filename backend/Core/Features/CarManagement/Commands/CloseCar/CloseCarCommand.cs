using Shared.CQRS;

namespace Features.CarManagement.Commands.CloseCar;

public record CloseCarCommand(int CarId) : ICommand;