using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.Balance.Commands.IncreaseBalance;

public class IncreaseBalanceCommandHandler : ICommandHandler<IncreaseBalanceCommand>
{
    private readonly CarsharingContext _context;

    public IncreaseBalanceCommandHandler(CarsharingContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(IncreaseBalanceCommand request, CancellationToken cancellationToken)
    {
        var user =  _context.UserInfos.First(x => x.UserId == request.UserId);
        user.Balance += request.Value;
        user.Verified = false;
        _context.UserInfos.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.SuccessResult;
    }
}