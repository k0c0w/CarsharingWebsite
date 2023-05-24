namespace Contracts;

public record RentCarDto
{
    public int CarId { get; init; }
    
    public int TariffId { get; init; }
    
    public int PotentialRenterUserInfoId { get; init; }
    
    public DateTime Start { get; init; }
    
    public DateTime End { get; init; }

    public int Days => (End - Start).Days;
}