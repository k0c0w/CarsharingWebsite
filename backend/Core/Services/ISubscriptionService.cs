using Domain.Entities;
using Shared.Results;

namespace Services;

public interface ISubscriptionService
{
    Task<Result> PrepareSubscriptionAsync(Subscription subscription);

    Task<Result> CommitAsync();

    Task<Result> RollbackAsync();
}
