using BalanceMicroservice.Clients;
using Contracts;
using Microsoft.Extensions.Logging;
using Services;
using Shared.Results;
using Result = Shared.Results.Result;

namespace Persistence.Services;

public class BalanceService : IBalanceService
{
    private readonly BalanceMicroservice.Clients.BalanceService.BalanceServiceClient _balanceServiceClient;
    private readonly ILogger<BalanceService> _logger;

    private BalanceChangeRequest? _balanceChangeRequest;

    private Transaction? _transaction;

    public BalanceService(BalanceMicroservice.Clients.BalanceService.BalanceServiceClient balanceServiceClient, ILogger<BalanceService> logger)
    {
        _balanceServiceClient = balanceServiceClient;
        _logger = logger;
    }

    public async Task<Result> CommitAsync()
    {
        try
        {
            var reply = await _balanceServiceClient.CommitTransactionAsync(_transaction);

            if (!reply.IsSuccess)
            {
                _logger.LogError("Balance change commit was aborted with message: {message}", reply.Message);

                return new Error(reply.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while commiting balance change transaction.");

            return Result.ErrorResult;
        }

        return Result.SuccessResult;
    }

    public async Task<Result> PrepareBalanceChangeAsync(string userId, decimal balanceChange)
    {
        if (balanceChange == 0)
        {
            _logger.LogError("Balance change must be non 0, but was {balanceChange}.", balanceChange);
            return new Error("Balance change must be at least 0.01");
        }

        var positiveChange = balanceChange > 0;
        balanceChange = Math.Abs(balanceChange);
        var integerPart = decimal.ToInt64(balanceChange);

        var request = new BalanceChangeRequest()
        {
            UserId = userId,
            BalanceChange = new DecimalValue()
            {
                IsPositive = positiveChange,
                IntegerPart = integerPart,
                FractionPart = decimal.ToInt32((balanceChange - integerPart) * 100),
            }
        };

        try
        {
            var reply = await _balanceServiceClient.PrepareTransactionAsync(request);

            if (reply.IsSuccessReply)
            {
                _balanceChangeRequest = request;
                _transaction = reply.Transaction;

                return Result.SuccessResult;
            }
            else
            {
                _logger.LogError("Balance change preparation was aborted with message: {message}", reply.Message);

                return Result.ErrorResult;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while preparing balance change transaction {balanceChange}.", balanceChange);
            return Result.ErrorResult;
        }
    }

    public async Task<Result> RollbackAsync()
    {
        try
        {
            await _balanceServiceClient.AbortTransactionAsync(_transaction);

            _transaction = default;
            _balanceChangeRequest = default;

            return Result.SuccessResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while aborting balance change transaction.");
            return Result.ErrorResult;
        }

    }
}
