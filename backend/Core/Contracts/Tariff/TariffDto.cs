namespace Contracts.Tariff;

public class TariffDto
{
    public int Id { get; init; }
    
    public string Name { get; init; }

    public string Description { get; init; }
    
    public decimal PriceInRubles { get; init; }
    
    public int? MaxMileage { get; init; }
}