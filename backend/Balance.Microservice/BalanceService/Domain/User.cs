using System.ComponentModel.DataAnnotations;
using BalanceService.Domain.ValueObjects;

namespace BalanceService.Domain;

public class User
{
    public UserId Id { get; private set; }

    public BalanceId BalanceId { get; init; }

    public Balance Balance { get; private set; }

    private User () {}
    
    public User(UserId id)
    {
        Id = id;
        BalanceId = new BalanceId(Guid.NewGuid());
    }
    
    public User(UserId id, BalanceId balanceId)
    {
        Id = id;
        BalanceId = balanceId;
    }
}
