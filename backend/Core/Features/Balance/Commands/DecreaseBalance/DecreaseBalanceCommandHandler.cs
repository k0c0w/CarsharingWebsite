using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.Balance.Commands.DecreaseBalance;

public class DecreaseBalanceCommandHandler : ICommandHandler<DecreaseBalanceCommand>
{
    private readonly CarsharingContext _context;

    public DecreaseBalanceCommandHandler(CarsharingContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DecreaseBalanceCommand request, CancellationToken cancellationToken)
    {
        var user =  _context.UserInfos.First(x => x.UserId == request.UserId);

        if (user!.Balance < request.Value)
            return new Error("Недостаточный баланс");
        
        user.Balance -= request.Value;
        user.Verified = false;
        _context.UserInfos.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.SuccessResult;
    }
}