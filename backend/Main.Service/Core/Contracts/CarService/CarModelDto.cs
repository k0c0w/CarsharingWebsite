namespace Contracts;

public record CarModelDto
{
    public int Id { get; init; }
    
    public string? Brand { get; set; }
    
    public string? Model { get; set; }

    public string? Description { get; set; }

    public int TariffId { get; set; }

    public string? ImageUrl { get; init; }
}