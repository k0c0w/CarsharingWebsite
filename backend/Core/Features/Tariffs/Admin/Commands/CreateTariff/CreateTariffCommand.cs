using Shared.CQRS;

namespace Features.Tariffs.Admin;

public class CreateTariffCommand : ICommand
{
    public string Name { get; } = string.Empty;

    public string? Description { get; }

    public decimal? PriceInRubles { get; }

    public int? MaxMileage { get; }


    public CreateTariffCommand(string tariffName, decimal? priceInRubles = default, string? description = default, int? maxMileage = default)
    {
        Name = tariffName;
        Description = description;
        PriceInRubles = priceInRubles;
        MaxMileage = maxMileage;
    }
}
