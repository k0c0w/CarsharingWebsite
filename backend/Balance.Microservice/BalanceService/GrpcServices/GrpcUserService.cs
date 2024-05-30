using AutoMapper;
using BalanceMicroservice.Clients;
using BalanceService.Domain;
using BalanceService.Domain.Abstractions.DataAccess;
using BalanceService.Domain.ValueObjects;
using Grpc.Core;

namespace BalanceService.GrpcServices;

public class GrpcUserService : UserManagementService.UserManagementServiceBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IBalanceRepository _balanceRepository;
    private const string UserNotExists = "User does not Exists";

    public GrpcUserService(IMapper mapper, IUserRepository userRepository, IBalanceRepository balanceRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _balanceRepository = balanceRepository;
    }

    public override async Task<Result> AddUser(GrpcUserRequest request, ServerCallContext context)
    {
        var user = _mapper.Map<GrpcUserRequest, User>(request);
        var result = new Result()
        {
            IsSuccess = true,
            Message = ""
        };
        
        try
        {
            await _userRepository.AddAsync(user, context.CancellationToken);
        }
        catch (Exception e)
        {
            result.IsSuccess = false;
            result.Message = e.Message;
        }

        return result;
    }

    public override async Task<Result> RemoveUser(GrpcUserRequest request, ServerCallContext context)
    {
        var user = _mapper.Map<GrpcUserRequest, User>(request);
        var result = new Result()
        {
            IsSuccess = true,
            Message = ""
        };
        
        try
        {
            await _userRepository.RemoveAsync(user, context.CancellationToken);
        }
        catch (Exception e)
        {
            result.IsSuccess = false;
            result.Message = e.Message;
        }

        return result;
    }

    public override async Task<UserInfo> GetUserInfo(GrpcUserRequest request, ServerCallContext context)
    {
        var balance = await _balanceRepository.GetBalanceByUserIdAsync(new UserId(request.Id));
        var decimalValue = new DecimalValue()
        {
            IsPositive = balance.Savings > 0,
            IntegerPart = (long)Math.Floor(balance.Savings),
            FractionPart = (int)(100.0m * (balance.Savings - Math.Floor(balance.Savings)))
        };

        return new UserInfo()
        {
            UserId = request.Id,
            Balance = decimalValue
        };
    }

    public override async Task<Result> UserExists(GrpcUserRequest request, ServerCallContext context)
    {
        var user = await _userRepository.GetByIdAsync(new UserId(request.Id), context.CancellationToken);

        return new Result()
        {
            IsSuccess = user is not null,
            Message = user is not null ? string.Empty : UserNotExists
        };
    }
}