using AutoMapper;
using Carsharing.Hubs.ChatEntities;
using Carsharing.ViewModels.Admin.User;
using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Carsharing.Controllers;
[Route("api/admin/user")]
[ApiController]
public class AdminUserController: ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<UserRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly IDictionary<string, UserConnection> _connections;

    public AdminUserController(
        UserManager<User> userManager, 
        RoleManager<UserRole> roleManager,
        IMapper mapper,
        IDictionary<string, UserConnection> connections)
    {
        _mapper = mapper;
        _connections = connections;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    [Route("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        return new JsonResult(_userManager.Users.Select(x => new UserDto()
        {
            Id = x.Id,
            Email = x.Email,
            UserName = x.FirstName,
            Surname = x.Surname
        }));
    }
    
    [HttpPost]
    [Route("editnameorsurname/{id}")]
    public async Task<IActionResult> EditFirstNameOrSecondName([FromBody]EditUserNameOrSurnameVM editUserNameOrSurnameVm,[FromRoute]string id)
    {
        try
        {
            var old = await _userManager.FindByIdAsync(id);
            if (editUserNameOrSurnameVm.FirstName != null)
                old.FirstName = editUserNameOrSurnameVm.FirstName;
            
            if (editUserNameOrSurnameVm.SecondName != null)
                old.Surname = editUserNameOrSurnameVm.SecondName;

            var result = await _userManager.UpdateAsync(old);
            
            return NoContent();
            
        }
        catch (Exception e)
        {
            
            return NotFound();
        }
    }
    
    [HttpPost]
    [Route("editrole/{id}/{role}")]
    public async Task<IActionResult> EditUserRole([FromRoute]string role,[FromRoute]string id)
    {
        await _roleManager.CreateAsync(new UserRole{Name = "Admin"});
        try
        {
            //нужно будет добавить ограничения кто кому может менять роль
            var user = await _userManager.FindByIdAsync(id);
        
            var userRole = await _userManager.GetRolesAsync(user);

            var newUserRole = await _roleManager.FindByNameAsync(role);

            var removeRole = userRole.FirstOrDefault();

            await _userManager.AddToRoleAsync(user, newUserRole.Name);
            
            if (removeRole != null)
            {
                await _userManager.RemoveFromRoleAsync(user, removeRole);
            }
            
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }


    [HttpGet("getOpenChats")]
    public async Task<IActionResult> GetOpenChats()
    {
        var openConn = _connections.Where(elem => elem.Value.IsOpen)
            .Select(elem =>
            {
                var userId = elem.Value.Room.UserId;
                var user = _userManager.FindByIdAsync(userId).GetAwaiter().GetResult();

                return new OpenChatsVM()
                {
                    FirstName = user.FirstName,
                    LastName = user.Surname,
                    UserId = userId,
                    ConnectionId = elem.Key
                };
            });

        return new JsonResult(openConn);
    }
    
}