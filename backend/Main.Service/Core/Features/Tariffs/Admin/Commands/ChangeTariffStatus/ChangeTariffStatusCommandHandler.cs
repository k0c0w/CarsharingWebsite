using Domain.Repository;
using Entities.Repository;
using Microsoft.Extensions.Logging;

namespace Features.Tariffs.Admin;

public class ChangeTariffStatusCommandHandler : ICommandHandler<ChangeTariffStatusCommand>
{
    private readonly IUnitOfWork<ITariffRepository> _tariffs;
    private readonly ILogger<ChangeTariffStatusCommandHandler> _logger;

    public ChangeTariffStatusCommandHandler(IUnitOfWork<ITariffRepository> repository, ILogger<ChangeTariffStatusCommandHandler> logger)
    {
       _tariffs = repository;
        _logger = logger;
    }

    public async Task<Result> Handle(ChangeTariffStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var tariff = await _tariffs.Unit.GetByIdAsync(command.Tariffd);

            if (tariff == null)
                return new Error("Tariff was not found.");

            tariff.IsActive = command.TurnOn;
            await _tariffs.Unit.UpdateAsync(tariff);
            await _tariffs.SaveChangesAsync();

            return Result.SuccessResult;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while updating tariff state. {exception}", ex);
            return Result.ErrorResult;
        }
    }
}
