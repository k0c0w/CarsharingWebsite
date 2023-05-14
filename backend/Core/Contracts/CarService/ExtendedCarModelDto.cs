namespace Contracts;

public record ExtendedCarModelDto : CarModelDto
{
    public string TariffName { get; init; }
    
    public decimal Price { get; init; }
    
    public int? Restrictions { get; init; }
}