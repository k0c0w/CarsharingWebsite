using System.Data.Common;
using AutoMapper;
using BalanceMicroservice.Clients;
using BalanceService.Domain.Abstractions.DataAccess;
using BalanceService.Domain.ValueObjects;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace BalanceService.GrpcServices;

public class GrpcBalanceService : BalanceMicroservice.Clients.BalanceService.BalanceServiceBase
{
    private readonly IBalanceRepository _balanceRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private TransactionMemory _transactionMemory;
    private readonly IUserRepository _userRepository;
    private const string UserNotExistsError = "User not exists";
    private const string PrevTransError = "Previous Transaction is still in process";
    private const string TransNotExistsError = "Such Transaction does not exist";

    public GrpcBalanceService(IBalanceRepository balanceRepository, IUserRepository userRepository, TransactionMemory transactionMemory, ITransactionRepository transactionRepository, IMapper mapper)
    {
        _balanceRepository = balanceRepository;
        _userRepository = userRepository;
        _transactionMemory = transactionMemory;
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public override async Task<TransactionInfo> PrepareTransaction(BalanceChangeRequest request, ServerCallContext context)
    {
        var result = new TransactionInfo()
        {
            IsSuccessReply = false,
            Message = string.Empty
        };

        var user = await _userRepository.GetByIdAsync(new UserId(request.UserId), context.CancellationToken);
        if (user is null)
        {
            result.Message = UserNotExistsError;
            return result;
        }

        if (_transactionMemory.Find(new UserId(request.UserId)) is not null)
        {
            result.Message = PrevTransError;
            return result;
        }

        try
        {
            var transaction = _mapper.Map<BalanceChangeRequest, Domain.Transaction>(request);
            transaction.BalanceId = user.BalanceId;
            result.Transaction = new Transaction() { Id = transaction.Id.Value };
            await _transactionRepository.AddAsync(transaction);
        }
        catch (DbException e)
        {
            result.Message = e.Message;
            return result;
        }
        
        result.IsSuccessReply = true;
        return result;
    }

    public override async Task<Result> CommitTransaction(Transaction request, ServerCallContext context)
    {
        var result = new Result()
        {
            IsSuccess = false,
            Message = string.Empty
        };

        var transaction = await _transactionRepository.GetByIdWithUserAndBalanceAsync(new TransactionId(request.Id));
        if (transaction is null)
        {
            result.Message = TransNotExistsError;
            return result;
        }
        
        var userId = transaction.Balance.UserId;
        
        try
        {
            var balanceChange = (transaction.IsPositive ? 1 : -1) * (transaction.IntegerPart + transaction.FractionPart / 100m);

            await _balanceRepository.ChangeBalanceAsync(userId, balanceChange,
                context.CancellationToken);
        }
        catch (Exception e)
        {
            result.Message = e.Message;
            return result;
        }
        finally
        {
            _transactionMemory.RemoveTransaction(userId);
        }

        result.IsSuccess = true;
        return result;
    }

    public override async Task<Empty> AbortTransaction(Transaction request, ServerCallContext context)
    {
        var transaction = await _transactionRepository.GetByIdWithUserAndBalanceAsync(new TransactionId(request.Id));
        if (transaction is null)
        {
            return new Empty();
        }

        var userId = transaction.Balance.UserId;
        _transactionMemory.RemoveTransaction(userId);
        
        if (transaction.Commited)
        {
            var balanceChange = (transaction.IsPositive ? 1 : -1) * (transaction.IntegerPart + transaction.FractionPart / 100m);

            await _balanceRepository.ChangeBalanceAsync(userId, -balanceChange,
                context.CancellationToken);
        }
       
        return new Empty();
    }

    public override async Task<DecimalValue> GetBalance(GrpcUserRequest request, ServerCallContext context)
    {
        var balance = await _balanceRepository.GetBalanceByUserIdAsync(new UserId(request.Id));
        var integerPart = decimal.ToInt64(balance.Savings);
        var result = new DecimalValue()
        {
            IsPositive = balance.Savings > 0,
            IntegerPart = integerPart,
            FractionPart = decimal.ToInt32(100.0m * (balance.Savings - integerPart))
        };

        return result;
    }
}