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
    public async Task<IActionResult> EditUserNameOrSurname([FromBody]EditUserNameOrSurnameVM editUserNameOrSurnameVm,[FromRoute]string id)
    {
        try
        {
            //почему-то говорит что юзернаме кривой
            var old = await _userManager.FindByIdAsync(id);
            if (editUserNameOrSurnameVm.UserName != null)
                old.UserName = editUserNameOrSurnameVm.UserName;
            
            if (editUserNameOrSurnameVm.Surname != null)
                old.Surname = editUserNameOrSurnameVm.Surname;

            var result = await _userManager.UpdateAsync(old);
            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
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