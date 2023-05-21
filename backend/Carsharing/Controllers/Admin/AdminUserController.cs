using Carsharing.ViewModels.Admin.UserInfo;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Carsharing.Controllers;

[ApiController]
[Route("/admin/api/[controller]")]
public class AdminUserController: ControllerBase
{
    private readonly IUserInfoService _userInfoService;

    public AdminUserController(IUserInfoService userInfoService)
    {
        _userInfoService = userInfoService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> All()
    {
        var users = await _userInfoService.GetAllInfoAsync();
        return new JsonResult( users.Select(x =>new UserInfoVm
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
                    UserName = x.User.UserName,
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber
                }
            })
        );
    }

    [HttpPut("verify/{id:int}")]
    public async Task<IActionResult> VerifyUserChanges([FromRoute]int id)
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

}