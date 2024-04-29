using Entities.Repository;
using FluentValidation;
using MediatR;
using Shared.Results;

namespace Features.CarBooking.Commands.BookCar;

public class BookCarValidationBehavior : IPipelineBehavior<BookCarCommand, Result>
{
    private readonly IEnumerable<IValidator<BookCarCommand>> _commandValidators;

    private readonly IUserRepository _userRepository;
    private readonly ICarRepository _carRepository;

    public BookCarValidationBehavior(IEnumerable<IValidator<BookCarCommand>> validators, IUserRepository userRepository, ICarRepository carRepository)
    {
        _commandValidators = validators;
        _userRepository = userRepository;
        _carRepository = carRepository;
    }

    public async Task<Result> Handle(BookCarCommand request, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken)
    {
        foreach (var item in _commandValidators)
        {
            var validationResult = await item.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return validationResult.Errors.Any() ? new Error(validationResult.Errors.Select(x => x.ErrorMessage).First()) : Result.ErrorResult;
            }
        }

        var userInfo = await _userRepository.GetUserInfoByUserIdAsync(request.RentCarInfo.PotentialRenterUserId!);
        if (userInfo == null || !userInfo.Verified)
            return new Error("Verify personal data first.");

        var tariff = await _carRepository.GetRelatedTariffAsync(request.RentCarInfo.CarId);
        if (tariff == null || !tariff.IsActive)
            return new Error("Car is busy.");

        return await next();
    }
}
