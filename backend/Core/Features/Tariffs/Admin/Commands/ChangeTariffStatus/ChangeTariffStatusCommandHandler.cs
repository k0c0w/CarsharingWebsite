﻿using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.Tariffs.Admin;

public class ChangeTariffStatusCommandHandler : ICommandHandler<ChangeTariffStatusCommand>
{
    private readonly ITariffRepository _tariffs;

    public ChangeTariffStatusCommandHandler(ITariffRepository repository)
    {
       _tariffs = repository;
    }

    public async Task<Result> Handle(ChangeTariffStatusCommand command, CancellationToken cancellationToken)
    {
        var tariff = await _tariffs.GetByIdAsync(command.Tariffd).ConfigureAwait(false);

        if (tariff == null)
            return new Error("Tariff was not found.");

        tariff.IsActive = command.TurnOn;
        await _tariffs.UpdateAsync(tariff).ConfigureAwait(false);

        return Result.SuccessResult;
    }
}
