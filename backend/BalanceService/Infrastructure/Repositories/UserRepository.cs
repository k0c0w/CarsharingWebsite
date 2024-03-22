using BalanceService.Domain;
using BalanceService.Domain.Abstractions.DataAccess;
using BalanceService.Domain.ValueObjects;
using BalanceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BalanceService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BalanceContext _balanceContext;

    public UserRepository(BalanceContext balanceContext)
    {
        _balanceContext = balanceContext;
    }

    public async Task AddAsync(User user, CancellationToken token)
    {
        var balance = new Balance(user.Id, user.BalanceId);
        await _balanceContext.Balances.AddAsync(balance, token);
        await _balanceContext.Users.AddAsync(user, token);
        await _balanceContext.SaveChangesAsync(token);
    }

    public async Task RemoveAsync(User user, CancellationToken token)
    {
        await _balanceContext.Users.Where(x=>x.Id==user.Id).ExecuteDeleteAsync(token);
        await _balanceContext.SaveChangesAsync(token);
    }

    public async Task<User> RemoveAsync(UserId userId, CancellationToken token)
    {
        var user = new User(userId);
        _balanceContext.Attach(user);
        _balanceContext.Users.Remove(user);
        await _balanceContext.SaveChangesAsync(token);

        return user;
    }

    public Task<User> LoadBalance(User user)
    {
        _balanceContext.Entry(user).Reference(p => p.Balance).Load();

        return Task.FromResult(user);
    }

    public async Task<User?> GetByIdAsync(UserId userId, CancellationToken token)
    {
        return await _balanceContext.Users.FindAsync(userId);
    }

    public Task<IQueryable<User>> GetAllAsync(CancellationToken token)
    {
        return Task.FromResult(_balanceContext.Users.AsQueryable());
    }
}