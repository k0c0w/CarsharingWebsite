using System.Text.RegularExpressions;
using Carsharing.Forms;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml;
using AutoMapper;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using Carsharing.Persistence.GoogleAPI;
using Carsharing.ViewModels;
using Carsharing.ViewModels.Admin.UserInfo;
using Domain.Entities;
using Domain;
using Contracts.UserInfo;
using Services.Abstractions;

namespace Carsharing.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
//[ValidateAntiForgeryToken]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly CarsharingContext _carsharingContext;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUserInfoService _userInfoService;

    public AccountController(
        CarsharingContext carsharingContext,
        UserManager<User> userManager,
        IMapper mapper,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        IUserInfoService userInfoService
        )
    {
        _mapper = mapper;
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _carsharingContext = carsharingContext;
        _userInfoService = userInfoService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegistrationDto dto)
    {

        var client = await _userManager.FindByEmailAsync(dto.Email);
        if (client != null)
        {
            return BadRequest("A client with this email already exists");
        }

        var _userInfo = new UserInfo() { BirthDay = dto.Birthday };
        var userInfo = await _carsharingContext.UserInfos.AddAsync(_userInfo);
        User user = _mapper.Map<User>(dto);
        user.UserInfo = userInfo.Entity;

        var resultUserCreate = await _userManager.CreateAsync(user, dto.Password);

        if (resultUserCreate.Succeeded != true)
            return Unauthorized(resultUserCreate.Errors);

        _userInfo.UserId = user.Id;
        _carsharingContext.Update(_userInfo);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.DateOfBirth, _userInfo?.BirthDay.ToString() ?? ""),
            new Claim("Passport", _userInfo?.Passport?.ToString() ?? "")
        };

        var pr = await _signInManager.CreateUserPrincipalAsync(user);
        pr.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        await _signInManager.SignInAsync(user, false);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pr);

        _carsharingContext.SaveChanges();

        return Ok();
    }

    // Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return Unauthorized("Почта или пароль неверен.");

        var resultSignIn = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);

        if (!resultSignIn.Succeeded)
        {
            return Unauthorized("Почта или пароль неверен.");
        }

        var userInfo = await _carsharingContext.UserInfos.FirstOrDefaultAsync(x => x.UserId == user.Id);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.DateOfBirth, userInfo?.BirthDay.ToString() ?? ""),
            new Claim("Passport", userInfo?.Passport?.ToString() ?? "")
        };

        var pr = await _signInManager.CreateUserPrincipalAsync(user);
        pr.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pr);

        return Ok();
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("google-external-auth-callback")]
    public async Task<IActionResult> GoogleExternalLoginCallback([FromQuery] string code, [FromQuery] string scope)
    {
        object locker = new object();
        try
        {
            var getTokenResult = await GoogleAPI.GetTokenAsync(code,
                _configuration["Authorization:Google:AppId"] ?? "",
                _configuration["Authorization:Google:AppSecret"] ?? "",
                "https://localhost:7129/api/account/google-external-auth-callback"
                );
            if (getTokenResult is null)
                return StatusCode(500, "Не получилось получить доступ к сервису Google");

            var getUserResult = await GoogleAPI.GetUserAsync(tokenId: getTokenResult.id_token, accessToken: getTokenResult.access_token);

            if (getUserResult is null)
                return StatusCode(500, "Не получилось получить доступ к сервису Google");

            User user = _mapper.Map<User>(getUserResult);
            UserInfo userInfo = null;


            if (await _userManager.FindByEmailAsync(user.Email) is null)
            {
                ///TODO: выделить создание юзера и юзеринфо в сервис и сделать lock

                UserInfo _userInfo = _mapper.Map<UserInfo>(getUserResult);
                var userInfoDb = await _carsharingContext.UserInfos.AddAsync(_userInfo);
                user.UserInfo = userInfoDb.Entity;

                var createUserResult = await _userManager.CreateAsync(user);
                if (createUserResult.Succeeded == true)
                {
                    userInfoDb.Entity.UserId = user.Id;
                    userInfo = _carsharingContext.UserInfos.Update(userInfoDb.Entity).Entity;
                }
                else
                {
                    return BadRequest(createUserResult.Errors);
                }
                _carsharingContext.SaveChanges();
            }


            if (userInfo is null)
            {
                try
                {
                    userInfo = await _carsharingContext.UserInfos.SingleAsync(entity => entity.UserId == user.Id);
                }
                catch { }
            }
            List<Claim> claims = new List<Claim>()
            {
            //new Claim(ClaimTypes.DateOfBirth, userInfo?.BirthDay.ToString() ?? ""), 1992-04-19 11:25:07.53 + 04
            new Claim(ClaimTypes.DateOfBirth, "1992-04-19 11:25:07.53+04"),
            new Claim("Passport", userInfo?.Passport?.ToString() ?? "passport")
            };

            var pr = await _signInManager.CreateUserPrincipalAsync(user);
            pr.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            await _signInManager.SignInWithClaimsAsync(user, false, claims);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pr);


            return Redirect("http://localhost:3000/profile");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Redirect("https://localhost:3000/login/");
            //return StatusCode(500, "Не получилось получить доступ к сервису Google");
        }

    }

    // Logout
    [HttpPost("logout")]
    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }

    [HttpPut("/edit/{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id,[FromBody] EditUserVm userVm)
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
            return new JsonResult(new {result ="Success"});
        }
        return new JsonResult(new
        {
            error = "Вы ввели неверные данные, в связи с чем произошла ошибка на сервере"
        });
    }
}


//{
//  "email": "DioBrando003@yandex.ru",
//  "password": "test00009AAAA111111++",
//  "retryPassword": "test00009AAAA111111++",
//  "userName": "Marsel",
//  "userSurname": "Alm",
//  "birthday": "2023-04-19T07:25:07.530Z"
//}

//{
//  "email": "DioBr003@yandex.ru",
//  "password": "test0000AAAA111111++"
//}