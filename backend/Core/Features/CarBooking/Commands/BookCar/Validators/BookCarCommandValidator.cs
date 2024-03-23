using Contracts;
using Entities.Repository;
using FluentValidation;

namespace Features.CarBooking.Commands.BookCar.Validators;

public class BookCarCommandValidator : AbstractValidator<BookCarCommand>
{
    private readonly ICarRepository _carRepository;

    public BookCarCommandValidator(ICarRepository carRepository)
    {
        _carRepository = carRepository;

        RuleFor(x => x.RentCarInfo)
            .NotNull()
            .Must(x => x.TariffId > 0)
            .WithMessage("Wrong tariff id.")
            .MustAsync((x, ct) => carRepository.ExistsByIdAsync(x.CarId))
            .WithMessage("Car does not exist.")
            .Must(x => x.Start <= x.End)
            .MustAsync((x, ct) => AreCorrectDateBoundsAsync(x))
            .WithMessage("Wrong date bounds.");

        RuleFor(x => x.RentCarInfo.PotentialRenterUserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Provide renter user id");
    }

    private async Task<bool> AreCorrectDateBoundsAsync(RentCarDto rentCarDto)
    {
        var tariff = await _carRepository.GetRelatedTariffAsync(rentCarDto.CarId);

        if (tariff == null)
            return false;

        var requestedRentMinutes = (rentCarDto.End - rentCarDto.Start).TotalMinutes;

        return requestedRentMinutes >= tariff.MinAllowedMinutes && requestedRentMinutes <= tariff.MaxAllowedMinutes;
    }
}