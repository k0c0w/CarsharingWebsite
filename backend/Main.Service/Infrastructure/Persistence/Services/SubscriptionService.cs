using Domain.Entities;
using Domain.Repository;
using Entities.Repository;
using Microsoft.Extensions.Logging;
using Services;
using Result = Results.Result;

namespace Persistence.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ILogger<SubscriptionService> _logger;
    private readonly IUnitOfWork<ISubscriptionRepository> _unitOfWork;

    private Subscription? _savedSubscription;
    private bool _savedInDb;

    public SubscriptionService(IUnitOfWork<ISubscriptionRepository> unitOfWork, ILogger<SubscriptionService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> CommitAsync()
    {
        if (_savedSubscription == null)
            return Result.ErrorResult;

        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while saving subscription.");

            return Result.ErrorResult;
        }

        _savedInDb = true;

        return Result.SuccessResult;
    }

    public async Task<Result> PrepareSubscriptionAsync(Subscription subscription)
    {
        try
        {
            await _unitOfWork.Unit.AddAsync(subscription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while adding subscription.");

            return Result.ErrorResult;
        }

        _savedSubscription = subscription;

        return Result.SuccessResult;
    }

    public async Task<Result> RollbackAsync()
    {
        if (_savedSubscription == null)
            return Result.ErrorResult;

        try
        {
            await _unitOfWork.Unit.RemoveAsync(_savedSubscription);
            if (_savedInDb)
            {
               await _unitOfWork.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while rollback subscription with {id}.", _savedSubscription?.SubscriptionId);

            return Result.ErrorResult;
        }

        _savedSubscription = null;
        _savedInDb = false;

        return Result.SuccessResult;
    }
}
