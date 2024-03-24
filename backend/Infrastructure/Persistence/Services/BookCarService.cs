using Domain.Repository;
using Entities.Repository;
using Microsoft.Extensions.Logging;
using Services;
using System.Data.Common;
using Shared.Results;
using Result = Shared.Results.Result;

namespace Persistence.Services;

public class BookCarService : IBookCarService
{
    private readonly IUnitOfWork<ICarRepository> _carRepositoryUoW;
    private readonly ILogger<BookCarService> _logger;

    private readonly SemaphoreSlim _locker = new(1,1);

    private int? _lockedCar;
    private bool _carCommited;

    public BookCarService(IUnitOfWork<ICarRepository> carRepositoryUoW, ILogger<BookCarService> logger)
    {
        _carRepositoryUoW = carRepositoryUoW;
        _logger = logger;
    }

    public Task<Result> CommitCarAssigmentAsync()
        => DoUnderLockAsync(CommitCarAssigmentInternalAsync());

    public Task<Result> PrepareCarAssigmentAsync(int carId)
        => DoUnderLockAsync(PrepareCarAssigmentInternalAsync(carId));

    public Task<Result> RollbackCarAssigmentAsync()
        => DoUnderLockAsync(RollbackCarAssigmentInternalAsync());

    private async Task<Result> PrepareCarAssigmentInternalAsync(int carId)
    {
        if (_lockedCar != null)
            return Result.ErrorResult;

        try
        {
            var car = await _carRepositoryUoW.Unit.GetByIdAsync(carId);
            if (car == null || car.HasToBeNonActive || car.IsTaken || car.Prebooked)
                return new Error("Car is already busy.");

            car.Prebooked = true;
            await _carRepositoryUoW.Unit.UpdateAsync(car);

            await _carRepositoryUoW.SaveChangesAsync();
        }
        catch (DbException ex)
        {
            _logger.LogError(ex, "Error during Car preparement.");

            return Result.ErrorResult;
        }

        _lockedCar = carId;

        return Result.SuccessResult;
    }

    private async Task<Result> RollbackCarAssigmentInternalAsync()
    {
        if (!_lockedCar.HasValue)
            return Result.ErrorResult;
        try
        {
            var car = await _carRepositoryUoW.Unit.GetByIdAsync(_lockedCar.Value);
            if (car == null)
                return Result.ErrorResult;

            car.Prebooked = false;
            if (_carCommited)
            {
                car.IsTaken = false;
            }

            await _carRepositoryUoW.Unit.UpdateAsync(car);
            await _carRepositoryUoW.SaveChangesAsync();
        }
        catch (DbException ex)
        {
            _logger.LogError(ex, "Error during Car {id} release.", _lockedCar.Value);

            return Result.ErrorResult;
        }

        _carCommited = default;
        _lockedCar = default;

        return Result.SuccessResult;
    }

    private async Task<Result> CommitCarAssigmentInternalAsync()
    {
        // no prepare was called or car already commited
        if (!_lockedCar.HasValue || _carCommited)
            return Result.ErrorResult;

        try
        {
            var car = await _carRepositoryUoW.Unit.GetByIdAsync(_lockedCar.Value);
            if (car == null || car.Prebooked)
                return new Error("Car is already busy.");

            car.Prebooked = false;
            car.IsTaken = true;
            await _carRepositoryUoW.Unit.UpdateAsync(car);

            await _carRepositoryUoW.SaveChangesAsync();
        }
        catch (DbException ex)
        {
            _logger.LogError(ex, "Error during Car commit.");

            return Result.ErrorResult;
        }

        _carCommited = true;

        return Result.SuccessResult;
    }

    private async Task<Result> DoUnderLockAsync(Task<Result> action)
    {
        await _locker.WaitAsync();
        try
        {
            return await action;
        }
        finally
        {
            _locker.Release();
        }
    }
}
