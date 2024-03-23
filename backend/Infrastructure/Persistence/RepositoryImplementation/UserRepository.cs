using Domain.Entities;
using Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.RepositoryImplementation;

public class UserRepository : IUserRepository
{
    public Task<string> AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetBatchAsync(int? offset = null, int? limit = null)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(string primaryKey)
    {
        throw new NotImplementedException();
    }

    public Task<UserInfo?> GetUserInfoByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveByIdAsync(string primaryKey)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }
}
