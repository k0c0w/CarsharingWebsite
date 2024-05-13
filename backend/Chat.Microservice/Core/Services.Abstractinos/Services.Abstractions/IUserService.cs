using Domain;

namespace Services.Abstractions;

public interface IUserService
{
    Task TryCreateUserAsync(User user);
}
