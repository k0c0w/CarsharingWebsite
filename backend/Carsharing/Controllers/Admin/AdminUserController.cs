using Carsharing.Helpers;
using Carsharing.ViewModels.Admin.UserInfo;
using Contracts.UserInfo;
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
    private readonly IBalanceService _balanceService;

    public AdminUserController(IBalanceService balanceService, IUserService userInfoService, UserManager<User> userManager, RoleManager<UserRole> roleManager)
    {
        _userInfoService = userInfoService;
        _roleManager = roleManager;
        _userManager = userManager;
        _balanceService = balanceService;
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
    
    [HttpGet("{id:required}")]
    public async Task<IActionResult> GetUser([FromRoute] string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();
        var user = await _userInfoService.GetUserInfoByIdAsync(id);
        if (user == null) return NotFound(ServiceError("No such user"));
        return new JsonResult( new UserVM
            {
                Email = user.User.Email,
                Id = user.UserId,
                EmailConfirmed = user.User.EmailConfirmed,
                FirstName = user.User.FirstName,
                LastName = user.User.LastName,
                PersonalInfo = new UserInfoVM
                {
                    Balance = user.Balance,
                    Passport = user.PassportType != null ? $"{user.PassportType} {user.Passport}" : null,
                    Verified = user.Verified,
                    BirthDay = DateOnly.FromDateTime(user.BirthDay),
                    DriverLicense = user.DriverLicense
                }
            }
        );
    }

    [HttpPut("Edit/{id:required}")]
    public async Task<IActionResult> EditUser([FromRoute] string id, [FromBody] EditUserVM edit)
    {
        if (string.IsNullOrEmpty(id)) return NotFound(ServiceError("No such user"));
        var result = await _userInfoService.EditUser(id, new EditUserDto()
        {
            Email = edit.email,
            FirstName = edit.name,
            BirthDay = edit.birthdate,
            LastName = edit.surname
        });
        if (result)
            return NoContent();

        return new JsonResult(ServiceError("Часть информации не сохранена"));
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
    
    [HttpGet("increase")]
    public async Task<IActionResult> IncreaseBalance([FromQuery] string id, [FromQuery] decimal val)
    {
        var result = await _balanceService.IncreaseBalance(id, val);

        if (result == "success")
        {
            return new JsonResult(new
            {
                result = $"Success, your Balance increased on {val}"
            });
        }

        return new JsonResult(new
        {
            result = "Не удалось пополнить баланс"
        });

    }

    [HttpGet("decrease")]
    public async Task<IActionResult> DecreaseBalance([FromQuery] string id, [FromQuery] decimal val)
    {
        var result = await _balanceService.DecreaseBalance(id, val);

        if (result == "success")
        {
            return new JsonResult(new
            {
                result = $"Success, your Balance increased on {val}"
            });
        }

        return new JsonResult(new
        {
            result = "Не удалось пополнить баланс"
        });
    }
    
    [HttpPost("{id:required}/GrantRole/{role:required}")]
    public async Task<IActionResult> GrantRole([FromRoute]string role,[FromRoute]string id)
    {
        //нужно будет добавить ограничения кто кому может менять роль
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound(ServiceError("No such user"));

        var newUserRole = await _roleManager.FindByNameAsync(role);
        if (newUserRole == null) return NotFound(ServiceError("No such role"));
        
        var userRole = await _userManager.GetRolesAsync(user);
        if (userRole.Contains(newUserRole.Name))
            return Ok();

        await _userManager.AddToRoleAsync(user, newUserRole.Name);

        return Ok();
    }
    
    [HttpDelete("{id:required}/RevokeRole/{role:required}")]
    public async Task<IActionResult> RevokeRole([FromRoute]string role,[FromRoute]string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound(ServiceError("No such user"));

        var newUserRole = await _roleManager.FindByNameAsync(role);
        if (newUserRole == null) return NotFound(ServiceError("No such role"));
    
        var userRole = await _userManager.GetRolesAsync(user);
        if (!userRole.Contains(newUserRole.Name))
            return NoContent();
        
        await _userManager.RemoveFromRoleAsync(user, newUserRole.Name);

        return NoContent();
    }

    private object ServiceError(string message)
    {
        return new { error = new { code = (int)ErrorCode.ServiceError, message = message } };
    }
}