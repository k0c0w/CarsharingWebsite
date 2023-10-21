using Domain.Entities;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.Tariffs.Admin;

public class UpdateTariffCommandHandler : ICommandHandler<UpdateTariffCommand>
{
    private readonly ITariffRepository _tariffs;

    public UpdateTariffCommandHandler(ITariffRepository tariffs)
    {
        _tariffs = tariffs;
    }

    public async Task<Result> Handle(UpdateTariffCommand command, CancellationToken cancellationToken)
    {
        var validateParamsResult = ValidateParams(command);

        if (!validateParamsResult)
            return validateParamsResult;

        try
        {
            var tariff = await _tariffs.GetByIdAsync(command.TariffId);

            if (tariff == null)
                return new Error("Tariff was not found.");

            UpdateModel(tariff, command);

            await _tariffs.UpdateAsync(tariff).ConfigureAwait(false);

            return Result.SuccessResult;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    private Result ValidateParams(UpdateTariffCommand command)
    {
        //todo: validate params

        return Result.SuccessResult;
    }

    private void UpdateModel(Tariff tariff, UpdateTariffCommand source) 
    {
        if (source.Name != null)
            tariff.Name = source.Name;

        if (source.Description != null)
            tariff.Description = source.Description;

        if (source.PriceInRubles != null)
            tariff.Price = source.PriceInRubles.Value;

        tariff.MaxMileage ??= source.MaxMileage;
    }
}
