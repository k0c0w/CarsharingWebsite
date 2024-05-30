using BalanceService.Domain;
using BalanceService.Domain.Abstractions.DataAccess;
using BalanceService.Domain.ValueObjects;
using BalanceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BalanceService.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly BalanceContext _balanceContext;

    public TransactionRepository(BalanceContext balanceContext)
    {
        _balanceContext = balanceContext;
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _balanceContext.Transactions.AddAsync(transaction);
        await _balanceContext.SaveChangesAsync();
    }

    public async Task<Transaction?> RemoveAsync(TransactionId transactionId)
    {
        var expr = _balanceContext.Transactions.Where(x=>x.Id == transactionId);
        var transaction = await expr.SingleOrDefaultAsync();
        await expr.ExecuteDeleteAsync();
        await _balanceContext.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction?> GetByIdAsync(TransactionId transactionId)
    {
        return await _balanceContext.Transactions.FindAsync(transactionId);
    }
    
    public async Task<Transaction?> GetByIdWithUserAndBalanceAsync(TransactionId transactionId)
    {
        return await _balanceContext.Transactions
            .Where(x=>x.Id==transactionId)
            .Include(x=>x.Balance)
            .ThenInclude(x=>x.User)
            .SingleOrDefaultAsync();
    }

    public async Task<Transaction> LoadBalanceAsync(Transaction transaction)
    {
        await _balanceContext.Entry(transaction).Reference(p => p.Balance).LoadAsync();

        return transaction;
    }
}