using Domain.Repositories;
using Domain;
using Microsoft.Extensions.Logging;
using Services.Abstractions;

namespace Persistence.ServicesImplementations;

public class UserService(IUserRepository userRepository, ILogger<UserService> logger) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly ILogger<UserService> _logger = logger;

    public async Task TryCreateUserAsync(User user)
    {
        try
        {
            var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
            if (existingUser == null)
            {
                _logger.LogInformation("User (id:{id}) already exists.", user.Id);
                return;
            }

            await _userRepository.AddAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError("User {user} was not created: {ex}", user, ex);
        }
    }
}