using Domain.Entities;
using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.Tariffs.Admin;

public class CreateTariffCommandHandler : ICommandHandler<CreateTariffCommand>
{
    private readonly ITariffRepository _tariffs;

    public CreateTariffCommandHandler(ITariffRepository tariffs) 
    {
        _tariffs = tariffs;
    }

    public async Task<Result> Handle(CreateTariffCommand command, CancellationToken cancellationToken)
    {
        var validationResult = AreValidParameters(command);

        if (!validationResult)
            return validationResult;

        var newTariff = new Tariff()
        {
            Name = command.Name,
            Description = command.Description ?? string.Empty,
            ImageUrl = "",
            MaxMileage = command.MaxMileage,
            Price = command.PriceInRubles ?? 1000,
            IsActive = false,
        };

        try
        {
            await _tariffs.AddAsync(newTariff).ConfigureAwait(false);
            return Result.SuccessResult;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    private Result AreValidParameters(CreateTariffCommand command)
    {
        if (string.IsNullOrEmpty(command.Name))
            return new Error("Tariff name was empty.");

        if (command.Name.Length > 512)
            return new Error("Tariff name was too long.");

        return Result.SuccessResult; 
    }
}
