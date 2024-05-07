using Domain;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Migrations;

namespace Persistence.RepositoriesImplementations;

public class UserRepository(ChatServiceContext context) : IUserRepository
{
    private readonly ChatServiceContext _ctx = context;

    public Task<User?> GetUserByIdAsync(string id)
        => _ctx
        .User
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<User>> GetUsersByIdsAsync(params string[] ids)
        => await _ctx
            .User
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .ToArrayAsync();
}
