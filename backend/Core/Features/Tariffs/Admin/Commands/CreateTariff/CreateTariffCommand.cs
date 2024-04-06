using Shared.CQRS;

namespace Features.Tariffs.Admin;

public class CreateTariffCommand : ICommand
{
    public string Name { get; } = string.Empty;

    public string? Description { get; }

    public decimal? PriceInRubles { get; }

    public int? MaxMileage { get; }

    public long MinAllowedMinutes { get; }

    public long MaxAllowedMinutes { get; }


    public CreateTariffCommand(
        string tariffName, 
        decimal? priceInRubles = default,
        string? description = default,
        int? maxMileage = default, 
        long minAllowedMinutes = 5,
        long maxAllowedMinutes = 24 * 60)
    {
        Name = tariffName;
        Description = description;
        PriceInRubles = priceInRubles;
        MaxMileage = maxMileage;
        MinAllowedMinutes = minAllowedMinutes;
        MaxAllowedMinutes = maxAllowedMinutes;
    }
}
