using System.Security.Claims;
using Carsharing.ViewModels;
using Carsharing.ViewModels.Profile;
using Contracts.UserInfo;
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
    
    [HttpPut("/edit/{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EditUserVm userVm)
    {
        var result = await _userInfoService.EditUser(id, new EditUserDto
        {
            UserSurname = userVm.UserSurname,
            UserName = userVm.UserName,
            BirthDay = userVm.BirthDay,
            Email = userVm.Email,
            PhoneNumber = userVm.PhoneNumber,
            Passport = userVm.Passport,
            PassportType = userVm.PassportType,
            DriverLicense = userVm.DriverLicense
        });
        if (result)
        {
            return new JsonResult(new { result = "Success" });
        }

        return new JsonResult(new
        {
            error = "Вы ввели неверные данные, в связи с чем произошла ошибка на сервере"
        });
    }
}