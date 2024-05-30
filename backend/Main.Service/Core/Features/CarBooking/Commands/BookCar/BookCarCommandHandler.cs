using Carsharing.Contracts.CarEvents;
using Domain.Entities;
using Domain.Repository;
using Entities.Repository;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services;
using System.Diagnostics;

namespace Features.CarBooking.Commands.BookCar;

public class BookCarCommandHandler : ICommandHandler<BookCarCommand>
{
    private readonly ICarRepository _carRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IBalanceService _balanceService;
    private readonly ISubscriptionService _subscriptionService;
    private readonly IBookCarService _bookCarService;
    private readonly ILogger<BookCarCommandHandler> _logger;

    public BookCarCommandHandler(ILogger<BookCarCommandHandler> logger, 
        ICarRepository carRepository, 
        ISubscriptionService subscriptionService, 
        IBookCarService bookCarService,
        IPublishEndpoint publishEndpoint,
        IUnitOfWork unitOfWork,
        IBalanceService balanceService)
    {
        _carRepository = carRepository;
        _balanceService = balanceService;
        _bookCarService = bookCarService;
        _subscriptionService = subscriptionService;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(BookCarCommand request, CancellationToken cancellationToken)
    {
        var rentInfo = request.RentCarInfo;

        var tariff = await _carRepository.GetRelatedTariffAsync(rentInfo.CarId);
        var totalPrice = -tariff!.PricePerMinute * request.RentCarInfo.Days;
        var userId = rentInfo.PotentialRenterUserId!;

        var subscription = new Subscription()
        {
            CarId = rentInfo.CarId,
            StartDate = rentInfo.Start,
            EndDate = rentInfo.End,
            UserId = rentInfo.PotentialRenterUserId!,
            IsActive = true,
        };

        var tasksToComplete = new Task<Result>[]
        {
            _balanceService.PrepareBalanceChangeAsync(userId, totalPrice),
            _subscriptionService.PrepareSubscriptionAsync(subscription),
            _bookCarService.PrepareCarAssignmentAsync(rentInfo.CarId),
        };

        await Task.WhenAll(tasksToComplete);

        Debug.Assert(tasksToComplete.All(x => x.IsCompleted));
        if (tasksToComplete.Any(x => !x.Result.IsSuccess))
        {
            var message = "Unable to book car.";

            var debitResult = tasksToComplete[0].Result;
            if (!debitResult.IsSuccess)
                message = debitResult.ErrorMessage ?? "Error occurred while attempting to debit funds. Check your balance first.";

            await RollbackAsync();

            _logger.LogInformation("Rollback after prepared.");

            return new Error(message);
        }

        _logger.LogInformation("Prepared");

        tasksToComplete[0] = _balanceService.CommitAsync();
        tasksToComplete[1] = _subscriptionService.CommitAsync();
        tasksToComplete[2] = _bookCarService.CommitCarAssignmentAsync();

        await Task.WhenAll(tasksToComplete);

        if (tasksToComplete.Any(x => !x.Result.IsSuccess))
        {
            await RollbackAsync();

            _logger.LogInformation("Rollback after commit.");

            return new Error("Unable to complete operation.");
        }

        var car = await _carRepository.GetByIdAsync(rentInfo.CarId);
        Debug.Assert(car != null, "Car was not found.");
        Debug.Assert(car.CarModel != null, "Include Car Model.");
        await _publishEndpoint.Publish(new CarBookedEvent
        {
            TariffName = tariff.Name,
            CarLicensePlate = car.LicensePlate,
            CarModelName = $"{car.CarModel.Brand} {car.CarModel.Model}",
            SubscriptionStartTimeUtc = subscription.StartDate,
            CreationTimeUtc = DateTime.UtcNow,
        });
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Committed.");

        return Result.SuccessResult;
    }

    private Task RollbackAsync()
        => Task.WhenAll(_balanceService.RollbackAsync(), _subscriptionService.RollbackAsync(), _bookCarService.RollbackCarAssignmentAsync());
}