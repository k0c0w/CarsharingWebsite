using Carsharing.Helpers;
using Carsharing.ViewModels;
using Carsharing.ViewModels.Profile;
using Contracts.UserInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Carsharing.Controllers;

[Route("Api/Account")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IUserService _userService;
    public ProfileController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var info = await _userService.GetProfileInfoAsync(User.GetId());
        return new JsonResult(new ProfileInfoVM
        {
            UserInfo = new UserInfoVM
            {
                Balance = info.PersonalInfo.Balance,
                Email = info.PersonalInfo.Email,
                FullName = $"{info.PersonalInfo.FirstName} {info.PersonalInfo.LastName}"
            },
            BookedCars = info.CurrentlyBookedCars.Select(x => new ProfileCarVM
            {
                Name = x.Model,
                IsOpened = x.IsOpened,
                LicensePlate = x.LicensePlate,
            })
        });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM change)
    {
        var info = await _userService.ChangePassword(User.GetId(), change.OldPassword, change.Password);
        if (info.Success) return NoContent();

        return BadRequest(new { error = new { code = (int)ErrorCode.ServiceError, messages = info.Errors } });
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> PersonalInfo()
    {
        var info = await _userService.GetPersonalInfoAsync(User.GetId());
        return new JsonResult(new PersonalInfoVM()
        {
            Email = info.Email,
            Passport = info.Passport,
            Surname = info.LastName,
            BirthDate = DateOnly.FromDateTime(info.BirthDate),
            DriverLicense = info.DriverLicense,
            FirstName = info.FirstName
        });
    }
    
    [HttpPut("edit")]
    public async Task<IActionResult> Edit([FromBody] EditUserVm userVm)
    {
        var result = await _userService.EditUser(User.GetId(), new EditUserDto
        {
            LastName = userVm.LastName,
            FirstName = userVm.FirstName,
            BirthDay = userVm.BirthDay,
            Email = userVm.Email,
            Passport = userVm.Passport?.Substring(4),
            PassportType = userVm.Passport?.Substring(0, 4),
            DriverLicense = userVm.DriverLicense
        });
        if (result)
            return NoContent();
        
        return new BadRequestObjectResult(new {error=new
        {
            code = (int)ErrorCode.ServiceError,
            messages= new [] { "Одно или несколько полей содержат некорректные данные."}
        }});
    }
}