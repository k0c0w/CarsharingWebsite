using Carsharing.ViewModels.Admin.User;
using Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Carsharing.Controllers;
[Route("user")]
[ApiController]
public class AdminUserController: ControllerBase
{
    private readonly UserService _userService;

    public AdminUserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("/users")]
    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        return await _userService.GetAllUsersAsync();
    }
    
    [HttpPost]
    [Route("/editnameorsurname/{id}")]
    public async Task<IActionResult> EditUserNameOrSurname([FromBody]EditUserNameOrSurnameVM editUserNameOrSurnameVm,[FromRoute]string id)
    {
        try
        {
            await _userService.EditUserNameOrSurnameAsync(new EditUserDto
            {
                UserName = editUserNameOrSurnameVm.UserName,
                Surname = editUserNameOrSurnameVm.Surname
            }, id);
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
            await _userService.EditUserRole(role, id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }
    
    
}