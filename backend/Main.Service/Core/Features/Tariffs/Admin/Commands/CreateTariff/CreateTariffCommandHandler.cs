using Domain.Entities;
using Domain.Repository;
using Entities.Repository;

namespace Features.Tariffs.Admin;

public class CreateTariffCommandHandler : ICommandHandler<CreateTariffCommand>
{
    private readonly IUnitOfWork<ITariffRepository> _tariffsUoW;

    public CreateTariffCommandHandler(IUnitOfWork<ITariffRepository> tariffsUoW) 
    {
        _tariffsUoW = tariffsUoW;
    }

    public async Task<Result> Handle(CreateTariffCommand command, CancellationToken cancellationToken)
    {
        var newTariff = new Tariff()
        {
            Name = command.Name,
            Description = command.Description ?? string.Empty,
            ImageUrl = "",
            MaxMileage = command.MaxMileage,
            PricePerMinute = command.PriceInRubles ?? 1000,
            IsActive = false,
            MinAllowedMinutes = command.MinAllowedMinutes,
            MaxAllowedMinutes = command.MaxAllowedMinutes,
        };

        await _tariffsUoW.Unit.AddAsync(newTariff);
        await _tariffsUoW.SaveChangesAsync();

        return Result.SuccessResult;
    }
}
