using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Commands.EditModel;

public record EditModelCommand(
    int Id, 
    EditCarModelDto EditCarModelDto) : ICommand;