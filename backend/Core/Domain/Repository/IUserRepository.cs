using Domain.Entities;
using Domain.Repository;

namespace Entities.Repository;

public interface IUserRepository : IRepository<User, string>
{
    Task<UserInfo?> GetUserInfoByUserIdAsync(string userId);

    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <exception cref="UserCreationException"
    /// <returns></returns>
    Task CreateUserAsync(User user, string password, params Role[] roles);
}
