using BalanceService.Domain.ValueObjects;

namespace BalanceService.Domain.Abstractions.DataAccess;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<Transaction?> RemoveAsync(TransactionId transactionId);
    Task<Transaction?> GetByIdWithUserAndBalanceAsync(TransactionId transactionId);
    Task<Transaction?> GetByIdAsync(TransactionId transactionId);
    Task<Transaction> LoadBalanceAsync(Transaction transaction);
}