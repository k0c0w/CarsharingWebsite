﻿using Domain.Entities;
using Entities.Repository;
using Services;
using Shared.CQRS;
using Shared.Results;
using System.Diagnostics;

namespace Features.CarBooking.Commands.BookCar;

public class BookCarCommandHandler : ICommandHandler<BookCarCommand>
{
    private readonly ICarRepository _carRepository;
    private readonly IBalanceService _balanceService;
    private readonly ISubscriptionService _subscriptionService;
    private readonly IBookCarService _bookCarService;


    public BookCarCommandHandler(ICarRepository carRepository, ISubscriptionService subscriptionService, IBookCarService bookCarService, IBalanceService balanceService)
    {
        _carRepository = carRepository;
        _balanceService = balanceService;
        _bookCarService = bookCarService;
        _subscriptionService = subscriptionService;
    }

    public async Task<Result> Handle(BookCarCommand request, CancellationToken cancellationToken)
    {
        var rentInfo = request.RentCarInfo;

        var tariff = await _carRepository.GetRelatedTariffAsync(rentInfo.CarId);
        var totalPrice = tariff!.PricePerMinute * request.RentCarInfo.Days;
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
            _bookCarService.PrepareCarAssigmentAsync(rentInfo.CarId),
        };

        await Task.WhenAll(tasksToComplete);

        Debug.Assert(tasksToComplete.All(x => x.IsCompleted));
        if (tasksToComplete.Any(x => !x.Result.IsSuccess))
        {
            var message = "Unable to book car.";

            var debitResult = tasksToComplete[0].Result;
            if (!debitResult.IsSuccess)
                message = debitResult.ErrorMessage ?? "Error occured while atempting to debit funds. Check your balance first.";

            await RollbackAsync();

            return new Error(message);
        }

        tasksToComplete[0] = _balanceService.CommitAsync();
        tasksToComplete[1] = _subscriptionService.CommitAsync();
        tasksToComplete[2] = _bookCarService.CommitCarAssigmentAsync();

        await Task.WhenAll(tasksToComplete);

        if (tasksToComplete.Any(x => !x.Result.IsSuccess))
        {
            await RollbackAsync();

            return new Error("Unable to complete operation.");
        }

        return Result.SuccessResult;
    }

    private Task RollbackAsync()
        => Task.WhenAll(_balanceService.RollbackAsync(), _subscriptionService.RollbackAsync(), _bookCarService.RollbackCarAssigmentAsync());
}