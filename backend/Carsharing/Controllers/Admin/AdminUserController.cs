using Carsharing.ViewModels.Admin.UserInfo;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Carsharing.Controllers;

[ApiController]
[Route("/api/admin/user")]
public class AdminUserController: ControllerBase
{
    private readonly IUserService _userInfoService;
    private readonly RoleManager<UserRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public AdminUserController(IUserService userInfoService, UserManager<User> userManager, RoleManager<UserRole> roleManager)
    {
        _userInfoService = userInfoService;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet("all")]
    public async Task<IActionResult> All()
    {
        var users = await _userInfoService.GetAllInfoAsync();
        return new JsonResult( users.Select(x =>new UserVM
            {
                Email = x.User.Email,
                Id = x.UserId,
                EmailConfirmed = x.User.EmailConfirmed,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                PersonalInfo = new UserInfoVM
                {
                    Balance = x.Balance,
                    Passport = x.PassportType != null ? $"{x.PassportType} {x.Passport}" : null,
                    Verified = x.Verified,
                    BirthDay = DateOnly.FromDateTime(x.BirthDay),
                    DriverLicense = x.DriverLicense
                }
            })
        );
    }

    [HttpPut("verify/{id:required}")]
    public async Task<IActionResult> VerifyUserChanges([FromRoute]string id)
    {
        var result  = await _userInfoService.Verify(id);
        if (result)
        {
            return new JsonResult(new { result = "Success"});
        }
        else
        {
            return new JsonResult(new {result = "Fail"});
        }
    }
    
    [HttpPost]
    [Route("editrole/{id}/{role}")]
    public async Task<IActionResult> EditUserRole([FromRoute]string role,[FromRoute]string id)
    {
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
}