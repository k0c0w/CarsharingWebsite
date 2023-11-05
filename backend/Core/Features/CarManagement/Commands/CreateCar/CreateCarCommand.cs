using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Commands.CreateCar;

public record CreateCarCommand(CreateCarDto CreateCarDto) : ICommand;
