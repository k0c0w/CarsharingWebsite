using Contracts.Results;
using Contracts.User;

namespace Services.Abstractions;

//todo: rename to IUserService
public interface IUserInfoService
{
    Task<UserInfoDto> GetPersonalInfoAsync(string userId);

    Task<ProfileInfoDto> GetProfileInfoAsync(string userId);

    Task<PasswordChangeResult>  ChangePassword(string userId, string oldPassword, string newPassword);
}