using FluentValidation;

namespace Features.CarBooking.Commands.BookCar;

public class BookCarCommandValidator : AbstractValidator<BookCarCommand>
{
    public BookCarCommandValidator()
    {
        RuleFor(x => x.RentCarInfo)
            .Must(rentCrInfo => rentCrInfo.Start <= rentCrInfo.End && rentCrInfo.Days != 0)
            .WithMessage("Wrong date bounds");
    }
}