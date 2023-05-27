using System.Collections.Immutable;
using Carsharing.ViewModels.Admin.UserInfo;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Carsharing.Controllers;

[ApiController]
[Route("/admin/api/[controller]")]
public class AdminUserController: ControllerBase
{
    private readonly IUserService _userInfoService;
    private readonly IBalanceService _balanceService;

    public AdminUserController(IBalanceService balanceService, IUserService userInfoService)
    {
        _userInfoService = userInfoService;
        _balanceService = balanceService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> All()
    {
        var users = await _userInfoService.GetAllInfoAsync();
        return new JsonResult( users.Select(x => new UserInfoVm
            {
                UserInfoId = x.UserInfoId,
                BirthDay = x.BirthDay,
                Passport = x.Passport,
                PassportType = x.PassportType,
                DriverLicense = x.DriverLicense,
                Balance = x.Balance,
                UserId = x.UserId,
                Verified = x.Verified,
                User = new User
                {
                    Id = x.User.Id,
                    LastName = x.User.LastName,
                    FirstName = x.User.FirstName,
                    UserName = x.User.UserName,
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber
                }
            })
        );
    }

    [HttpPut("verify/{id}")]
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
}