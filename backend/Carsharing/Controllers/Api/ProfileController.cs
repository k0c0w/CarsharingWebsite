using System.Security.Claims;
using Carsharing.ViewModels.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Carsharing.Controllers;

[Route("api/Account")]
// todo: uncomment atributte [Authorize]
[ApiController]
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
        //todo: var userId = User.GetId();
        var info = await _userInfoService.GetProfileInfoAsync("c8e37715-1f4c-45aa-aca8-bbfadcac21fe");
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

    [HttpGet("[action]")]
    public async Task<IActionResult> PersonalInfo()
    {
        //todo: var userId = User.GetId();
        var info = await _userInfoService.GetPersonalInfoAsync("c8e37715-1f4c-45aa-aca8-bbfadcac21fe");
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