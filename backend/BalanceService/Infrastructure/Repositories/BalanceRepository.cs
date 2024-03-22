using BalanceService.Domain;
using BalanceService.Domain.Abstractions.DataAccess;
using BalanceService.Domain.ValueObjects;
using BalanceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BalanceService.Infrastructure.Repositories;

public class BalanceRepository : IBalanceRepository
{
    private readonly BalanceContext _balanceContext;

    public BalanceRepository(BalanceContext balanceContext)
    {
        _balanceContext = balanceContext;
    }

    public async Task ChangeBalanceAsync(UserId userId, decimal amount, CancellationToken token)
    {
        var _user = await _balanceContext.Users.Include(x=>x.Balance).SingleAsync(x => x.Id == userId, cancellationToken: token);
        _user.Balance.CommitTransaction(amount);
        await _balanceContext.SaveChangesAsync(token);
    }
    
    public async Task<Balance> GetBalanceByIdAsync(BalanceId balanceId)
    {
        return await _balanceContext.Balances.SingleAsync(x => x.Id == balanceId);
    }

    public async Task<Balance> GetBalanceByUserIdAsync(UserId userId)
    {
       return await _balanceContext.Balances.SingleAsync(x => x.UserId == userId);
    }
}