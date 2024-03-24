using System.ComponentModel.DataAnnotations.Schema;
using BalanceService.Domain.ValueObjects;

namespace BalanceService.Domain;

public class Transaction
{
    public TransactionId Id { get; init; }
    [NotMapped] public decimal Amount { get; private set; }
    [NotMapped] private BalanceId _balanceId;
    public BalanceId BalanceId
    {
        get => _balanceId;
        set
        {
            if (_balanceId is not null)
                throw new Exception();
            _balanceId ??= value;
        }
    }
    public Balance Balance { get; init; }
    public bool IsPositive { get; set; }
    public long IntegerPart { get; set; }
    public int FractionPart { get; set; }

    public Transaction()
    {
        Id = new TransactionId(Guid.NewGuid().ToString());
    }

    public Transaction(BalanceId balanceId, decimal amount, Balance? balance = null)
    {
        Id = new TransactionId(Guid.NewGuid().ToString());
        BalanceId = balanceId;
        Amount = amount;
        
        if (balance != null) 
            Balance = balance;
    }
}