using Shared.CQRS;

namespace Features.CarManagement.Commands.ReleaseCar;

public record ReleaseCarCommand(int CarId) : ICommand;