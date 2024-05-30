namespace Features.Tariffs.Admin;

public class UpdateTariffCommand : ICommand
{
    public int TariffId { get; }

    public string? Name { get; }

    public string? Description { get; }

    public decimal? PriceInRubles { get; }

    public int? MaxMileage { get; }


    public UpdateTariffCommand(int tariffId, string? name = default, string? description = default, decimal? priceInRubles = default, int? maxMilage = default)
    {
        TariffId = tariffId;
        Name = name;
        Description = description;
        PriceInRubles = priceInRubles;
        MaxMileage = maxMilage;
    }
}
