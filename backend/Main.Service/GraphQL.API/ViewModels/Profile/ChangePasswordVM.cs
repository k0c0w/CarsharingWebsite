namespace GraphQL.API.ViewModels.Profile;

public record ChangePasswordVM
{
    public string? OldPassword { get; init; }
    
    public string? Password { get; init; }
}