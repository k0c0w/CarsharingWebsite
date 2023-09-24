using Contracts.UserInfo;
using Contracts.Results;
using Contracts.User;
using Domain.Entities;

namespace Services.Abstractions;

public interface IUserService
{
    Task<bool> EditUser(string userId, EditUserDto editUserDto);
    Task<bool> Verify(string userId);

    Task<List<UserInfo>> GetAllInfoAsync();
    
    Task<UserInfo> GetUserInfoByIdAsync(string id);
    
    Task<UserInfoDto> GetPersonalInfoAsync(string userId);

    Task<ProfileInfoDto> GetProfileInfoAsync(string userId);

    Task<PasswordChangeResult>  ChangePassword(string userId, string oldPassword, string newPassword);
}