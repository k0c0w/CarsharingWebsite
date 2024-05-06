using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsersByIdsAsync(params string[] ids);
    public Task<User> GetUserIdAsync(string id);
}
