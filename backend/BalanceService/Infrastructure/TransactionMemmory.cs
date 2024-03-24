using BalanceService.Domain.ValueObjects;
using System.Collections.Concurrent;

namespace BalanceService;

public class TransactionMemory
{
    private readonly ConcurrentDictionary<UserId, TransactionId> _dictionary = new();

    public bool AddTransaction(UserId userId, TransactionId transactionId) =>
        _dictionary.TryAdd(userId, transactionId);

    public void RemoveTransaction(UserId userId) =>
        _dictionary.Remove(userId, out var _);

    public TransactionId? Find(UserId userId)
    {
        _dictionary.TryGetValue(userId, out var transactionId);
        return transactionId;
    }
}