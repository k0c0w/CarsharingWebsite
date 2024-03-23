using AutoMapper;
using BalanceMicroservice.Clients;
using BalanceService.Domain;
using BalanceService.Domain.Abstractions.DataAccess;
using Grpc.Core;

namespace BalanceService.GrpcServices;

public class GrpcUserService : UserManagementService.UserManagementServiceBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GrpcUserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
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

        (await _userRepository.GetAllAsync(context.CancellationToken)).ToArray();
        
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

    public override Task<UserInfo> GetUserInfo(GrpcUserRequest request, ServerCallContext context)
    {
        //todo:
        return base.GetUserInfo(request, context);
    }

    public override Task<Result> UserExists(GrpcUserRequest request, ServerCallContext context)
    {
        //todo:
        return base.UserExists(request, context);
    }
}