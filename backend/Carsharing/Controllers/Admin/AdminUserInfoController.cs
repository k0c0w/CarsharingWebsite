using Carsharing.ViewModels.Admin.UserInfo;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Carsharing.Controllers;

[ApiController]
[Route("/admin/api/[controller]")]
public class AdminUserInfoController: ControllerBase
{
    private readonly IUserInfoService _userInfoService;

    public AdminUserInfoController(IUserInfoService userInfoService)
    {
        _userInfoService = userInfoService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllUserInfoAsync()
    {
        var users = await _userInfoService.GettAllInfoAsync();
        return new JsonResult( users.Select(x =>new UserInfoVm
            {
                UserInfoId = x.UserInfoId,
                BirthDay = x.BirthDay,
                Passport = x.Passport,
                PassportType = x.PassportType,
                DriverLicense = x.DriverLicense,
                Balance = x.Balance,
                UserId = x.UserId
            })
        );
    }
}