using BalanceService.Domain.Abstractions.DataAccess;
using BalanceService.Domain.ValueObjects;
using BalanceService.Infrastructure.Persistence;
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
            Message = String.Empty
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
            await _balanceRepository.ChangeBalanceAsync(new UserId(request.UserId), request.Value,
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
        await _balanceRepository.ChangeBalanceAsync(new UserId(request.UserId), request.Value * -1,
            context.CancellationToken);

        return new Empty();
    }
}