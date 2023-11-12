using Contracts;
using Shared.CQRS;

namespace Features.CarBooking.Commands.BookCar;

public record BookCarCommand(RentCarDto RentCarInfo) : ICommand;