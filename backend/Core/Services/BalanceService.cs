using Domain;
using Services.Abstractions;

namespace Services;

public class BalanceService : IBalanceService
{
    private readonly CarsharingContext _context;

    public BalanceService(CarsharingContext context)
    {
        _context = context;
    }
    
    public async Task<string> IncreaseBalance(string userId, decimal val)
    {
        var user =  _context.UserInfos.FirstOrDefault(x => x.UserId == userId);
        user.Balance += val;
        user.Verified = false;
        _context.UserInfos.Update(user);
        await _context.SaveChangesAsync();
        return "success";
    }

    public async Task<string> DecreaseBalance(string userId, decimal val)
    {
        var user =  _context.UserInfos.FirstOrDefault(x => x.UserId == userId);

        if (user.Balance < val)
        {
            throw new Exception("Недостаточный баланс");
        }

        user.Balance -= val;
        user.Verified = false;
        _context.UserInfos.Update(user);
        await _context.SaveChangesAsync();

        return "success";
    }
}