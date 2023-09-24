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
    private readonly IBalanceService _balanceService;
    private readonly ICarService _carService;
    public ProfileController(ICarService carService, IBalanceService balanceService, IUserService userService)
    {
        _userService = userService;
        _balanceService = balanceService;
        _carService = carService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var info = await _userService.GetProfileInfoAsync(User.GetId());
        return new JsonResult(new ProfileInfoVM
        {
            UserInfo = new UserInfoVM
            {
                Balance = info!.PersonalInfo!.Balance,
                Email = info.PersonalInfo.Email,
                FullName = $"{info.PersonalInfo.FirstName} {info.PersonalInfo.LastName}"
            },
            BookedCars = info!.CurrentlyBookedCars!.Select(x => new ProfileCarVM
            {
                Name = x.Model,
                IsOpened = x.IsOpened,
                LicensePlate = x.LicensePlate,
                ImageUrl = x.Image
            })
        });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM change)
    {
        var info = await _userService.ChangePassword(User.GetId(), change!.OldPassword!, change!.Password!);
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
            Phone = info.Phone,
            BirthDate = DateOnly.FromDateTime(info.BirthDate),
            DriverLicense = info.DriverLicense,
            FirstName = info.FirstName
        });
    }
    
    [HttpPut("PersonalInfo/Edit")]
    public async Task<IActionResult> Edit([FromBody] EditUserVm userVm)
    {
        var result = await _userService.EditUser(User.GetId(), new EditUserDto
        {
            LastName = userVm.LastName,
            FirstName = userVm.FirstName,
            BirthDay = userVm.BirthDay,
            Email = userVm.Email,
            PhoneNumber = userVm.PhoneNumber,
            Passport = userVm.Passport?.Substring(4),
            PassportType = userVm.Passport?.Substring(4),
            DriverLicense = userVm.DriverLicense
        });
        if (result)
        {
            return new JsonResult(new { result = "Success" });
        }
        
        return new BadRequestObjectResult(new {error=new
        {
            code = (int)ErrorCode.ServiceError,
            message = $"Ошибка сохранения"
        }});
    }
    
    [HttpGet("increase")]
    public async Task<IActionResult> IncreaseBalance([FromQuery] decimal val)
    {
        var result = await _balanceService.IncreaseBalance(User.GetId(), val);

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

    [HttpGet("open/{licensePlate:required}")]
    public async Task<IActionResult> OpenCar([FromRoute] string licensePlate)
    {
        var info = await _userService.GetProfileInfoAsync(User.GetId());
        var result = await _carService.OpenCar(info!.CurrentlyBookedCars!.Select(x => x).First(x => x.LicensePlate == licensePlate).Id);
        
        return new JsonResult(new
        {
            result
        });
    }
    
    [HttpGet("close/{licensePlate:required}")]
    public async Task<IActionResult> CloseCar([FromRoute] string licensePlate)
    {
        var info = await _userService.GetProfileInfoAsync(User.GetId());
        var result = await _carService.CloseCar(info!.CurrentlyBookedCars!.Select(x => x).First(x => x.LicensePlate == licensePlate).Id);
        
        return new JsonResult(new
        {
            result
        });
    }
}