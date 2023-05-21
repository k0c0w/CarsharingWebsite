using Contracts.UserInfo;
using Contracts.Results;
using Contracts.User;
using Domain.Entities;

namespace Services.Abstractions;

//todo: rename to IUserService
public interface IUserInfoService
{
    Task<bool> EditUser(int id, EditUserDto editUserVm);
    Task<bool> Verify(int id);

    Task<List<UserInfo>> GetAllInfoAsync();
    
    Task<UserInfoDto> GetPersonalInfoAsync(string userId);

    Task<ProfileInfoDto> GetProfileInfoAsync(string userId);

    Task<PasswordChangeResult>  ChangePassword(string userId, string oldPassword, string newPassword);
}