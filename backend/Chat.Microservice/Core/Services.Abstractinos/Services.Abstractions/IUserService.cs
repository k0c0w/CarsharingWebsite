using Domain;

namespace Services.Abstractions;

public interface IUserService
{
    Task TryCreateUserAsync(UserInfoDto user);

    Task TryDeleteUserAsync(string id);

    Task TryUpdateUserAsync(UserInfoDto userUpdate);
}
