using Carsharing.ViewModels.Admin.User;
using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Controllers;
[Route("user")]
[ApiController]
public class AdminUserController: ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<UserRole> _roleManager;

    public AdminUserController(UserManager<User> userManager, RoleManager<UserRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    [Route("/users")]
    public async Task<IActionResult> GetAllUsers()
    {
        return new JsonResult(_userManager.Users.Select(x => new UserDto()
        {
            Id = x.Id,
            Email = x.Email,
            UserName = x.UserName,
            Surname = x.Surname
        }));
    }
    
    [HttpPost]
    [Route("/editnameorsurname/{id}")]
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
    [Route("/editrole/{id}")]
    public async Task<IActionResult> EditUserRole([FromBody]string role,[FromRoute]string id)
    {
        try
        {
            //надо проверить
            var user = await _userManager.FindByIdAsync(id);
        
            var userRole = await _userManager.GetRolesAsync(user);

            var newUserRole = await _roleManager.FindByNameAsync(role);
            
            await _userManager.AddToRoleAsync(user, newUserRole.Name);

            await _userManager.RemoveFromRoleAsync(user, userRole.FirstOrDefault());
            
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }
    
    
}