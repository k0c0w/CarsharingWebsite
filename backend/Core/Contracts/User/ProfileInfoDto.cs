namespace Contracts.User;

public record ProfileInfoDto
{
    public UserInfoDto PersonalInfo { get; init; }
    
    public IEnumerable<CarShortcutDto> CurrentlyBookedCars { get; init; }
}