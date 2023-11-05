using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Commands.CreateModel;

public record CreateModelCommand(CreateCarModelDto CreateCarModelDto) : ICommand;
