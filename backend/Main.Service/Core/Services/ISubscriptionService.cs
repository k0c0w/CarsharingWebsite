using Domain.Entities;
using Results;

namespace Services;

public interface ISubscriptionService
{
    Task<Result> PrepareSubscriptionAsync(Subscription subscription);

    Task<Result> CommitAsync();

    Task<Result> RollbackAsync();
}
