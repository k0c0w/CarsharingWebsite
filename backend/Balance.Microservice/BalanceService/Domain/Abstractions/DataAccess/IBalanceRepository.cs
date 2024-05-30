using BalanceService.Domain.ValueObjects;

namespace BalanceService.Domain.Abstractions.DataAccess;

public interface IBalanceRepository
{
    public Task ChangeBalanceAsync(UserId userId, decimal amount, CancellationToken token);

    public Task<Balance?> GetBalanceByIdAsync(BalanceId balanceId);

    public Task<Balance?> GetBalanceByUserIdAsync(UserId userId);
    Task<Balance> LoadBalanceAsync(Balance balance);
}