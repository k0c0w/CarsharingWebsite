using System.Security.Claims;
using Carsharing.Helpers;
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
    private readonly IUserService _userService;
    private readonly IBalanceService _balanceService;
    public ProfileController(IBalanceService balanceService, IUserService userService)
    {
        _userService = userService;
        _balanceService = balanceService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        //todo: var userId = User.GetId();
        var info = await _userService.GetProfileInfoAsync("bb5f0757-7dc8-4189-9dc3-9f66e2f2420c");
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
        var info = await _userService.GetPersonalInfoAsync("bb5f0757-7dc8-4189-9dc3-9f66e2f2420c");
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
    
    [HttpPut("/edit/{id}")]
    public async Task<IActionResult> Edit([FromRoute] string id, [FromBody] EditUserVm userVm)
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
}