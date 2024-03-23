using BalanceService.Domain.Abstractions.DataAccess;
using BalanceService.Domain.ValueObjects;
using Contracts;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace BalanceService.GrpcServices;

public class GrpcBalanceService : Contracts.BalanceService.BalanceServiceBase
{
    private readonly IBalanceRepository _balanceRepository;
    private readonly IUserRepository _userRepository;
    private const string InvalidGuid = "Invalid Id of user";

    public GrpcBalanceService(IBalanceRepository balanceRepository, IUserRepository userRepository)
    {
        _balanceRepository = balanceRepository;
        _userRepository = userRepository;
    }

    public override async Task<PrepareTransactionResult> PrepareTransaction(BalanceRequest request, ServerCallContext context)
    {
        var result = new PrepareTransactionResult()
        {
            IsSuccess = true,
            Message = string.Empty
        };

        var user = await _userRepository.GetByIdAsync(new UserId(request.UserId), context.CancellationToken);

        if (user is not null)
            return result;

        result.IsSuccess = false;
        result.Message = InvalidGuid;
        return result;
    }

    public override async Task<CommitTransactionResult> CommitTransaction(BalanceRequest request, ServerCallContext context)
    {
        var result = new CommitTransactionResult()
        {
            IsSuccess = true,
            Message = string.Empty
        };
        
        try
        {
            var balanceChange = (request.IsPositive ? 1 : -1) * (request.IntegerPart + request.FractionPart / 100m);

            await _balanceRepository.ChangeBalanceAsync(new UserId(request.UserId), balanceChange,
                context.CancellationToken);
        }
        catch (Exception e)
        {
            result.IsSuccess = false;
            result.Message = e.Message;
        }

        return result;
    }

    public override async Task<Empty> AbortTransaction(BalanceRequest request, ServerCallContext context)
    {
        var balanceChange = (request.IsPositive ? 1 : -1) * (request.IntegerPart + request.FractionPart / 100m);

        await _balanceRepository.ChangeBalanceAsync(new UserId(request.UserId), -balanceChange,
            context.CancellationToken);

        return new Empty();
    }
}