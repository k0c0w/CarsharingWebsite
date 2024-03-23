using Domain.Entities;
using Domain.Repository;


namespace Entities.Repository;

public interface IUserRepository : IRepository<User, string>
{

    Task<UserInfo?> GetUserInfoByUserIdAsync(string userId); 
}
