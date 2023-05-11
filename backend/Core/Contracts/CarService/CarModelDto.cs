namespace Contracts;

public class CarModelDto
{
    public int Id { get; init; }
    
    public string Brand { get; set; }
    
    public string Model { get; set; }

    public string Description { get; set; }

    public int TariffId { get; set; }

    public string ImageUrl => $"models/{Brand}/{Model.Replace(" ", "_")}.png";
}