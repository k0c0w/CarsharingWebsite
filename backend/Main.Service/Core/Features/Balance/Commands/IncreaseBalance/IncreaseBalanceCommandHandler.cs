using Services;

namespace Features.Balance.Commands.IncreaseBalance;

public class IncreaseBalanceCommandHandler : ICommandHandler<IncreaseBalanceCommand>
{
    private readonly IBalanceService _balanceService;

    public IncreaseBalanceCommandHandler(IBalanceService balanceService)
    {
        _balanceService = balanceService;
    }

    public async Task<Result> Handle(IncreaseBalanceCommand request, CancellationToken cancellationToken)
    {
        await _balanceService.PrepareBalanceChangeAsync(request.UserId, Math.Abs(request.Value));
        await _balanceService.CommitAsync();

        return Result.SuccessResult;
    }
}