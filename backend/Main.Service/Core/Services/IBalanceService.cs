using Results;

namespace Services;

public interface IBalanceService
{
    Task<Result> PrepareBalanceChangeAsync(string userId, decimal balanceChange);

    Task<Result> CommitAsync();

    Task<Result> RollbackAsync();
}
