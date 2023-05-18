using Domain.Entities;

namespace Services.Abstractions;

public interface IUserInfoService
{
    public Task<List<UserInfo>> GettAllInfoAsync();
}