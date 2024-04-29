using Contracts;
using Entities.Repository;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Features.CarBooking.Commands.BookCar.Validators;

public class BookCarCommandValidator : AbstractValidator<BookCarCommand>
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<BookCarCommandValidator> _logger;

    public BookCarCommandValidator(ICarRepository carRepository, ILogger<BookCarCommandValidator> logger)
    {
        _carRepository = carRepository;
        _logger = logger;

        RuleFor(x => x.RentCarInfo)
            .NotNull()
            //.MustAsync(async (x, ct) => await carRepository.ExistsByIdAsync(x.CarId))
            //.WithMessage("Car does not exist.")
            .Must(rentCrInfo => rentCrInfo.Start <= rentCrInfo.End)
            //.MustAsync(async (x, _) => await IsInTariffRentTimeBoundsAsync(x))
            .WithMessage("Wrong date bounds");

        RuleFor(x => x.RentCarInfo.PotentialRenterUserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Provide renter user id");
    }

    private async Task<bool> IsInTariffRentTimeBoundsAsync(RentCarDto rentCarDto)
    {
        var relatedTariff = await _carRepository.GetRelatedTariffAsync(rentCarDto.CarId);
        var rentMinutes = (rentCarDto.End - rentCarDto.Start).TotalSeconds / 60;

        var isInTariffRentTimeBounds = relatedTariff != null && relatedTariff!.MinAllowedMinutes <= rentMinutes && rentMinutes <= relatedTariff!.MaxAllowedMinutes;

        if (!isInTariffRentTimeBounds)
            _logger.LogInformation("Wrong rent time  {time}", rentMinutes);

        return isInTariffRentTimeBounds;
    }
}