namespace Contracts.Tariff;

public class CreateTariffDto
{
    public string? Name { get; init; }

    public string? Description { get; init; }
    
    public decimal PriceInRubles { get; init; }
    
    public int? MaxMileage { get; init; }
}