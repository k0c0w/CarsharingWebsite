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
        var newTariff = new Tariff()
        {
            Name = command.Name,
            Description = command.Description ?? string.Empty,
            ImageUrl = "",
            MaxMileage = command.MaxMileage,
            Price = command.PriceInRubles ?? 1000,
            IsActive = false,
        };

        await _tariffs.AddAsync(newTariff).ConfigureAwait(false);
        return Result.SuccessResult;
    }
}
