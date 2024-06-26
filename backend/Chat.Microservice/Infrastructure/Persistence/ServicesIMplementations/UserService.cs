﻿using Domain.Repositories;
using Domain;
using Microsoft.Extensions.Logging;
using Services.Abstractions;

namespace Persistence.ServicesImplementations;

public class UserService(IUserRepository userRepository, ILogger<UserService> logger) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    private readonly ILogger<UserService> _logger = logger;

    public async Task TryCreateUserAsync(UserInfoDto user)
    {
        var newUser = new User
        {
            Id = user.Id,
            Name = user.Name,
            IsManager = user.Roles.Contains("Manager"),
        };

        try
        {
            var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
            if (existingUser != null)
            {
                _logger.LogInformation("User (id:{id}) already exists.", user.Id);
                return;
            }

            await _userRepository.AddAsync(newUser);
        }
        catch (Exception ex)
        {
            _logger.LogError("User {user} was not created: {ex}", user, ex);
        }
    }

    public Task TryDeleteUserAsync(string id)
    {
        return _userRepository.RemoveByIdAsync(id);
    }

    public async Task TryUpdateUserAsync(UserInfoDto userUpdate)
    {
        var user = await _userRepository.GetUserByIdAsync(userUpdate.Id);
        if (user == null)
            return;

        user.Name = userUpdate.Name;
        user.IsManager = userUpdate.Roles.Contains("Manager");

        await _userRepository.UpdateAsync(user);
    }
}