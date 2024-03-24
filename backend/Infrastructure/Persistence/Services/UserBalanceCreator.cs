using Services;
using BalanceMicroservice.Clients;
using static BalanceMicroservice.Clients.UserManagementService;
using Microsoft.Extensions.Logging;
using Shared.Results;
using Result = Shared.Results.Result;

namespace Persistence.Services;

public class UserBalanceCreator : IUserBalanceCreatorService
{
    private readonly UserManagementServiceClient _balanceClient;
    private readonly ILogger<UserBalanceCreator> _logger;

    private GrpcUserRequest? _request;
    private bool _commited;

    public UserBalanceCreator(ILogger<UserBalanceCreator> logger, UserManagementServiceClient balanceServiceClient)
    {
        _logger = logger;
        _balanceClient = balanceServiceClient;
    }

    public async Task<Result> CommitAsync()
    {
        try
        {
            var userCreationResult = await _balanceClient.AddUserAsync(_request!);
            if (!userCreationResult.IsSuccess)
                return Result.ErrorResult;

            _commited = true;

            return Result.SuccessResult;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error while commiting.");

            return Result.ErrorResult;
        }
    }

    public async Task<Result> PrepareBalanceCreationAsync(string userId)
    {
        var request = new GrpcUserRequest() { Id = userId };

        try
        {
            var result = await _balanceClient.UserExistsAsync(request);
            if (result.IsSuccess)
                return new Error("User balance already exists!");

            _request = request;

            return Result.SuccessResult;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error while preparing.");

            return Result.ErrorResult;
        }
    }

    public async Task<Result> RollbackAsync()
    {
        try
        {
            if (_commited)
            {
                await _balanceClient.RemoveUserAsync(_request!);
            }

            return Result.SuccessResult;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error while rollback of unssuccesful user {userId}.", _request!.Id);

            return Result.ErrorResult;
        }
        finally
        {
            _commited = false;
            _request = null;
        }
    }
}
