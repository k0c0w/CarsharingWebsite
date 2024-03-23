using BalanceMicroservice.Clients;
using BalanceService.Domain.Abstractions.DataAccess;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace BalanceService.GrpcServices;

public class GrpcBalanceService : BalanceMicroservice.Clients.BalanceService.BalanceServiceBase
{
    private readonly IBalanceRepository _balanceRepository;
    private readonly IUserRepository _userRepository;
    private const string InvalidGuid = "Invalid Id of user";

    public GrpcBalanceService(IBalanceRepository balanceRepository, IUserRepository userRepository)
    {
        _balanceRepository = balanceRepository;
        _userRepository = userRepository;
    }

    public override Task<TransactionInfo> PrepareTransaction(BalanceChangeRequest request, ServerCallContext context)
    {
        /* old one
         *  var result = new PrepareTransactionResult()
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
        */
        //todo: подготовить транзакцию. проверить, что юзер существует
        // заблокировать пользователя в таблице (inMemory, redis или mongo), связать с блокировкой некий Id транзакции, так же запомнить на сколько надо изменить баланс
        // если пользователь уже заблокирован, то отвергнуть запрос либо ждать высвобождения ресурса
        return base.PrepareTransaction(request, context);
    }

    public override Task<Result> CommitTransaction(Transaction request, ServerCallContext context)
    {
        /* old one 
         *  var result = new CommitTransactionResult()
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
        */

        // todo: по полученному id транзакции найти юзера 
        // выполнить операцию и вернуть результат выполнения
        return base.CommitTransaction(request, context);
    }

    public override Task<Empty> AbortTransaction(Transaction request, ServerCallContext context)
    {
        /* old
         *  var balanceChange = (request.IsPositive ? 1 : -1) * (request.IntegerPart + request.FractionPart / 100m);

        await _balanceRepository.ChangeBalanceAsync(new UserId(request.UserId), -balanceChange,
            context.CancellationToken);

        return new Empty();
        */
        // todo: release юзера если не было комита или откат если комит был, естественно что id транзакции должен быть тем же что и последяя транзакция. 
        // аборт комита можно сделать допустим в течение 30 секунд после него и если ресурс не заблокирован
        return base.AbortTransaction(request, context); 
    }

    public override Task<DecimalValue> GetBalance(GrpcUserRequest request, ServerCallContext context)
    {
        //todo: баланс юзера
        return base.GetBalance(request, context);
    }
}