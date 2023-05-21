using Carsharing.Helpers;
using Carsharing.ViewModels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Carsharing.Controllers;

[Route("Api/Account")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IUserInfoService _userInfoService;
    public ProfileController(IUserInfoService userInfoService)
    {
        _userInfoService = userInfoService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var info = await _userInfoService.GetProfileInfoAsync(User.GetId());
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
        var info = await _userInfoService.ChangePassword(User.GetId(), change.OldPassword, change.Password);
        if (info.Success) return NoContent();

        return BadRequest(new { error = new { code = (int)ErrorCode.ServiceError, messages = info.Errors } });
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> PersonalInfo()
    {
        //todo: var userId = User.GetId();
        var info = await _userInfoService.GetPersonalInfoAsync("7b6d1618-c5ac-43d2-95d7-81f1e7d7b289");
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
}