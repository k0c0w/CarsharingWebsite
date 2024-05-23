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
    private readonly ICarRepository _carRepository;
    private readonly IUnitOfWork _uoW;
    private readonly ILogger<BookCarService> _logger;

    private readonly SemaphoreSlim _locker = new(1,1);

    private int? _lockedCar;
    private bool _carCommitted;

    public BookCarService(ICarRepository carRepository, ILogger<BookCarService> logger, IUnitOfWork unitOfWork)
    {
        _uoW = unitOfWork;
        _carRepository = carRepository;
        _logger = logger;
    }

    public Task<Result> CommitCarAssignmentAsync()
        => DoUnderLockAsync(CommitCarAssignmentInternalAsync());

    public Task<Result> PrepareCarAssignmentAsync(int carId)
        => DoUnderLockAsync(PrepareCarAssignmentInternalAsync(carId));

    public Task<Result> RollbackCarAssignmentAsync()
        => DoUnderLockAsync(RollbackCarAssignmentInternalAsync());

    private async Task<Result> PrepareCarAssignmentInternalAsync(int carId)
    {
        if (_lockedCar != null)
            return Result.ErrorResult;

        try
        {
            var car = await _carRepository.GetByIdAsync(carId);
            if (car == null || car.HasToBeNonActive || car.IsTaken || car.Prebooked)
                return new Error("Car is already busy.");

            car.Prebooked = true;
            await _carRepository.UpdateAsync(car);

            await _uoW.SaveChangesAsync();
        }
        catch (DbException ex)
        {
            _logger.LogError(ex, "Error during Car preparation.");

            return Result.ErrorResult;
        }

        _lockedCar = carId;

        return Result.SuccessResult;
    }

    private async Task<Result> RollbackCarAssignmentInternalAsync()
    {
        if (!_lockedCar.HasValue)
            return Result.ErrorResult;
        try
        {
            var car = await _carRepository.GetByIdAsync(_lockedCar.Value);
            if (car == null)
                return Result.ErrorResult;

            car.Prebooked = false;
            if (_carCommitted)
            {
                car.IsTaken = false;
            }

            await _carRepository.UpdateAsync(car);
            await _uoW.SaveChangesAsync();
        }
        catch (DbException ex)
        {
            _logger.LogError(ex, "Error during Car {id} release.", _lockedCar.Value);

            return Result.ErrorResult;
        }

        _carCommitted = default;
        _lockedCar = default;

        return Result.SuccessResult;
    }

    private async Task<Result> CommitCarAssignmentInternalAsync()
    {
        // no prepare was called or car already committed
        if (!_lockedCar.HasValue || _carCommitted)
            return Result.ErrorResult;

        try
        {
            var car = await _carRepository.GetByIdAsync(_lockedCar.Value);
            car!.Prebooked = false;
            car.IsTaken = true;
            await _carRepository.UpdateAsync(car);

            await _uoW.SaveChangesAsync();
        }
        catch (DbException ex)
        {
            _logger.LogError(ex, "Error during Car commit.");

            return Result.ErrorResult;
        }

        _carCommitted = true;

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
