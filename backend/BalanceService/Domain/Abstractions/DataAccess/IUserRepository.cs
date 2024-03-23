using BalanceService.Domain.ValueObjects;

namespace BalanceService.Domain.Abstractions.DataAccess;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken token);

    Task RemoveAsync(User user, CancellationToken token);

    Task<User> RemoveAsync(UserId userId, CancellationToken token);

    Task<User> LoadBalanceAsync(User user);

    Task<User?> GetByIdAsync(UserId userId, CancellationToken token);

    Task<IQueryable<User>> GetAllAsync(CancellationToken token);
    Task<User?> GetUserWithBalanceAndTransactions(UserId userId, CancellationToken token);
}