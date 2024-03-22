using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BalanceService.Domain.ValueObjects;

namespace BalanceService.Domain;

public class Balance
{
    public BalanceId Id { get; init; }
    public decimal Savings { get; private set; }
    public UserId UserId { get; init; }
    public User User { get; init; }

    private Balance() {}
    
    public Balance(UserId userId, BalanceId? balanceId = null)
    {
        UserId = userId;
        Id = balanceId ?? new BalanceId(Guid.NewGuid());
    }

    public void CommitTransaction(decimal amount) => Savings += amount;
}

