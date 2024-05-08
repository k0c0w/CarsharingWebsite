using Domain.Entities;
using Domain.Repository;
using Entities.Exceptions;
using Entities.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using System.Security.Claims;
using MassTransit;
using Carsharing.Contracts;

namespace Persistence.RepositoryImplementation;

public class UserRepository : IUserRepository
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly CarsharingContext _ctx;
    private readonly UserManager<User> _userManager;

    public UserRepository(CarsharingContext context, UserManager<User> userManager, IPublishEndpoint publishEndpoint)
    {
        _ctx = context;
        _userManager = userManager;
        _publishEndpoint = publishEndpoint;
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

        await _publishEndpoint.Publish(new UserCreatedEvent
        {
            Name = user.FirstName!,
            Roles = actualRolesNames,
            UserId = user.Id
        });
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

    public Task RemoveByIdAsync(string primaryKey)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        _ctx.Users.Update(entity);

        return Task.CompletedTask;
    }
}
