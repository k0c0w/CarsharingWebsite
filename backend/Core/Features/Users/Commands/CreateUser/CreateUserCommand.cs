using Shared.CQRS;
namespace Features.Users.Commands.CreateUser;

public record CreateUserCommand : ICommand
{
    public string Email { get; init; } = "";

    public string LastName { get; init; } = "";

    public string Name { get; init; } = "";

    public DateTime Birthdate { get; init; }

    public string Password { get; init; } = "";
}
