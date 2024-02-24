using Contracts;
using Domain.Entities;
using Shared.CQRS;

namespace Features.CarBooking.Commands.AssignCarToUser;

public record AssignCarToUserCommand (UserInfo UserInfo, RentCarDto Details, decimal Withdrawal): ICommand;