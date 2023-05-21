using Contracts;

namespace Services.Abstractions.Admin;

public interface IAdminUserService
{
    public Task<IEnumerable<UserDto>> GetAllUsersAsync();
    public Task EditUserNameOrSurnameAsync(EditUserDto userDto,string id);
    public Task EditUserRole(string role, string id);
}