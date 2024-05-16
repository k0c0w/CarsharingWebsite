using Domain;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Migrations;

namespace Persistence.RepositoriesImplementations;

public class UserRepository(ChatServiceContext context) : IUserRepository
{
    private readonly ChatServiceContext _ctx = context;

    public async Task AddAsync(User user)
    {
        await _ctx.Users.AddAsync(user);
        await _ctx.SaveChangesAsync();
    }

    public Task<User?> GetUserByIdAsync(string id)
        => _ctx
        .Users
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<User>> GetUsersByIdsAsync(params string[] ids)
        => await _ctx
            .Users
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .ToArrayAsync();

    public Task RemoveByIdAsync(string id)
    {
        return _ctx.Users
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _ctx.Users.Update(user);
        await _ctx.SaveChangesAsync();
    }
}
