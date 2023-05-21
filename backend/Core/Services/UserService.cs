using Contracts;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions.Admin;

namespace Services;

public class UserService : IAdminUserService
{
    private readonly CarsharingContext _carsharingContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(CarsharingContext carsharingContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _carsharingContext = carsharingContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return await _userManager.Users.Select(x => new UserDto()
        {
            Id = x.Id,
            Email = x.Email,
            UserName = x.UserName,
            Surname = x.Surname
        }).ToListAsync();
    }

    public async Task EditUserNameOrSurnameAsync(EditUserDto userDto, string id)
    {
        
            var old = await _userManager.FindByIdAsync(id);
            if (userDto.UserName != null)
                old.UserName = userDto.UserName;
            if (userDto.Surname != null)
                old.Surname = userDto.Surname;
            await _userManager.UpdateAsync(old);
    }

    public async Task EditUserRole(string role, string id)
    {
        var old = await _userManager.FindByIdAsync(id);
        var oldRole = await _userManager.GetRolesAsync(old);
        //todo
    }
}