namespace Contracts.User;

public class UserInfoDto
{
    public string UserId { get; init; }
    
    public string FirstName { get; init; }
    
    public string LastName { get; init; }
    
    public DateTime BirthDate { get; init; }
    
    public string? PassportType { get; init; }
    
    public string? Passport { get; init; }
    
    public int? DriverLicense { get; init; }
    
    public decimal Balance { get; init; }
}