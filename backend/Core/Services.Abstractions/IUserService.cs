using Contracts.UserInfo;
using Contracts.Results;
using Contracts.User;
using Domain.Entities;

namespace Services.Abstractions;

public interface IUserService
{
    Task<string> EditUser(string id, EditUserDto editUserVm);
    Task<bool> Verify(string id);

    Task<List<UserInfo>> GetAllInfoAsync();
    
    Task<UserInfoDto> GetPersonalInfoAsync(string userId);

    Task<ProfileInfoDto> GetProfileInfoAsync(string userId);

    Task<PasswordChangeResult>  ChangePassword(string userId, string oldPassword, string newPassword);
}