using Domain.Entities;
using Domain.Repository;
using Entities.Repository;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Persistence.RepositoryImplementation;

public class UserRepository : IUserRepository
{
    private readonly CarsharingContext _ctx;

    public UserRepository(CarsharingContext context)
    {
        _ctx = context;
    }

    public async Task AddAsync(User entity)
    {
        await _ctx.Users.AddAsync(entity);
    }

    public async Task<IEnumerable<User>> GetBatchAsync(int? offset = null, int? limit = null)
    {
        IQueryable<User> users = _ctx.Users.AsNoTracking();

        if (offset.HasValue)
            users=users.Skip(offset.Value);
        if (limit.HasValue)
            users = users.Take(limit.Value);

        return await users
            .Include(x => x.UserInfo)
            .ToArrayAsync();
    }

    public Task<User?> GetByIdAsync(string primaryKey)
    {
        return _ctx.Users
            .Include(x => x.UserInfo)
            .SingleOrDefaultAsync(x => x.Id == primaryKey);
    }

    public Task<UserInfo?> GetUserInfoByUserIdAsync(string userId)
    {
        return _ctx.UserInfos
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserId == userId);
    }

    public Task RemoveByIdAsync(string primaryKey)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        _ctx.Users.Update(entity);

        return Task.CompletedTask;
    }

    Task IRepository<User, string>.AddAsync(User entity)
    {
        throw new NotImplementedException();
    }
}
