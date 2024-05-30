using Contracts;

namespace Features.CarBooking.Commands.BookCar;

public record BookCarCommand(RentCarDto RentCarInfo) : ICommand;