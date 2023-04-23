using System.Text.RegularExpressions;
using Carsharing.Forms;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Authorization;

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

    public AccountController(
        CarsharingContext carsharingContext,
        UserManager<User> userManager,
        SignInManager<User> signInManager
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _carsharingContext = carsharingContext;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegistrationDto dto)
    {   

        // Into service
        var client = await _userManager.FindByEmailAsync(dto.Email);
        if (client != null)
        {
            return BadRequest("A client with this email already exists");
        }

        var _userInfo = new UserInfo() { BirthDay = dto.Birthday };
        var userInfo = await _carsharingContext.UserInfos.AddAsync(_userInfo);

        var user = new User()
        {
            Email = dto.Email,
            UserName = dto.Name,
            UserInfo = userInfo.Entity
        }; //UserInfoId = userInfo.Entity.Id

        var resultUserCreate = await _userManager.CreateAsync(user, dto.Password);

        if (resultUserCreate.Succeeded != true)
            return BadRequest();

        _userInfo.UserId = user.Id;
        _carsharingContext.Update(_userInfo);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, dto.Name),
            new Claim(ClaimTypes.Email, dto.Email),
            new Claim(ClaimTypes.DateOfBirth, _userInfo?.BirthDay.ToString()),
            new Claim("Passport", _userInfo?.Passport?.ToString() ?? "")
        };



        var pr = await _signInManager.CreateUserPrincipalAsync(user);
        pr.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        var resultSignIn = await _signInManager.PasswordSignInAsync(user, dto.Password ?? "", false, false);
        if (resultSignIn.Succeeded == false)
            return Unauthorized("Почта или пароль неверен.");

        //var resultAddClaim = await _userManager.AddClaimsAsync(user, claims);
        //if ( resultAddClaim.Succeeded == false )
        //    return StatusCode(500, "Не удалось добавить claims");

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pr);

        _carsharingContext.SaveChanges();

        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto?.Email);

        if (user == null)
            return Unauthorized("Почта или пароль неверен.");

        var userInfo = await _carsharingContext.UserInfos.FirstOrDefaultAsync(x => x.UserId == user.Id);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim(ClaimTypes.Email, dto.Email),
            new Claim(ClaimTypes.DateOfBirth, userInfo?.BirthDay.ToString()),
            new Claim("Passport", userInfo?.Passport?.ToString() ?? "")
        };

        var pr = await _signInManager.CreateUserPrincipalAsync(user);
        pr.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        var resultSignIn = await _signInManager.PasswordSignInAsync(user, dto.Password ?? "", false, false);
        if ( resultSignIn.Succeeded == false)
            return Unauthorized("Почта или пароль неверен.");

        //var resultAddClaim = await _userManager.AddClaimsAsync(user, claims);
        //if ( resultAddClaim.Succeeded == false )
        //    return StatusCode(500, "Не удалось добавить claims");

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pr);

        return Ok();
    }
    [HttpPost("logout")]
    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }
}


//{
//  "email": "DioBrando003@yandex.ru",
//  "password": "test00009AAAA111111++",
//  "retryPassword": "test00009AAAA111111++",
//  "name": "Marsel",
//  "surname": "Alm",
//  "birthday": "2023-04-19T07:25:07.530Z"
//}

//{
//  "email": "DioBr003@yandex.ru",
//  "password": "test0000AAAA111111++"
//}