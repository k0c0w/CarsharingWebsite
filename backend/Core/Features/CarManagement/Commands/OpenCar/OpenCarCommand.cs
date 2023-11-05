using Shared.CQRS;

namespace Features.CarManagement.Commands.OpenCar;

public record OpenCarCommand(int CarId) : ICommand;