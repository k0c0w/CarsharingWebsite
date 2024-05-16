using Domain.Entities;
using Entities.Exceptions;
using Entities.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using System.Security.Claims;

namespace Persistence.RepositoryImplementation;

public class UserRepository : IUserRepository
{
    private readonly CarsharingContext _ctx;
    private readonly UserManager<User> _userManager;

    public UserRepository(CarsharingContext context, UserManager<User> userManager)
    {
        _ctx = context;
        _userManager = userManager;
    }

    public async Task AddAsync(User entity)
    {
        await _ctx.Users.AddAsync(entity);
    }

    public async Task CreateUserAsync(User user, string password, params Role[] roles)
    {
        var actualRolesNames = roles
            .Distinct()
            .Select(x => x.ToString())
            .ToArray();

        var creationResult = await _userManager.CreateAsync(user, password);

        if (!creationResult.Succeeded)
        {
            throw new UserCreationException(creationResult.Errors.Select(x => x.Description).FirstOrDefault()!);
        }

        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));
        foreach (var role in actualRolesNames)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
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

    public Task<User?> GetByEmailAsync(string email)
    {
        return _ctx.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
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

    public async Task<IEnumerable<Role>> GetUserRolesAsync(string userId)
    {
        var userRoleIds = _ctx.UserRoles
            .Where(x => x.UserId == userId)
            .Select(x => x.RoleId);

        var usersRoleNamesQuery = _ctx.Roles.Join(userRoleIds,
            r => r.Id,
            ur => ur,
            (r, _) => r.Name
            );

        var userClaimedRolesQuery = _ctx.UserClaims
            .Where(x => x.UserId == userId && x.ClaimType == ClaimsIdentity.DefaultRoleClaimType)
            .Select(x => x.ClaimValue);

        var roleNames = await userClaimedRolesQuery
            .Concat(userClaimedRolesQuery)
            .Distinct()
            .ToArrayAsync();

        return roleNames
            .Where(x => !string.IsNullOrEmpty(x))
            .Select(Enum.Parse<Role>);
    }

    public async Task RemoveByIdAsync(string primaryKey)
    {
        await Task.WhenAll(
            _ctx.UserClaims.Where(x => x.UserId == primaryKey).ExecuteDeleteAsync(),
            _ctx.UserRoles.Where(x => x.UserId == primaryKey).ExecuteDeleteAsync(),
            _ctx.UserInfos.Where(x => x.UserId == primaryKey).ExecuteDeleteAsync());

        await _ctx.Users.Where(x => x.Id == primaryKey).ExecuteDeleteAsync();
    }

    public Task UpdateAsync(User entity)
    {
        _ctx.Users.Update(entity);

        return Task.CompletedTask;
    }
}
