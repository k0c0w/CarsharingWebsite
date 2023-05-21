using Domain.Entities;
using Contracts.UserInfo;

namespace Services.Abstractions;

public interface IUserInfoService
{
    public Task<List<UserInfo>> GettAllInfoAsync();
    public Task<bool> EditUser(int id, EditUserDto editUserVm);
    public Task<UserInfo> FindUserInfo(int id);
    public Task<bool> Verify(int id);
}