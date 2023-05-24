using System.Security.Claims;
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
        return new JsonResult(new ProfileInfoVM()
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
        var info = await _userInfoService.ChangePassword(User.GetId(), change.OldPassword, change.Password);
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
    
    [HttpPut("/edit/{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EditUserVm userVm)
    {

        var result = await _userService.EditUser(id, new EditUserDto
        {
            LastName = userVm.LastName,
            FirstName = userVm.FirstName,
            BirthDay = userVm.BirthDay,
            Email = userVm.Email,
            PhoneNumber = userVm.PhoneNumber,
            Passport = userVm.Passport,
            PassportType = userVm.PassportType,
            DriverLicense = userVm.DriverLicense
        });
        if (result == "success")
        {
            return new JsonResult(new { result = "Success" });
        }
        
        return new JsonResult(new
        {
            error = "Вы ввели неверные данные, в связи с чем произошла ошибка на сервере",
            errorType = $"{result}"
        });
    }
}