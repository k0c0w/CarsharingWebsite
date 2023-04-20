using System.Text.RegularExpressions;
using Carsharing.Forms;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Carsharing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
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
    public async Task<IActionResult> RegisterUser([FromBody]RegistrationDto dto)
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
        _carsharingContext.Update<UserInfo>(_userInfo);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, dto.Name),
            new Claim(ClaimTypes.Email, dto.Email),
            new Claim(ClaimTypes.DateOfBirth, dto.Birthday.ToString())
        };

        await _signInManager.SignInWithClaimsAsync(user, true, claims);

        _carsharingContext.SaveChanges();

        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto?.Email);

        if (user == null)
            return Unauthorized("Email or password is incorrect");

        if (await _userManager.CheckPasswordAsync(user, dto?.Password) != true)
            return Unauthorized("Email or password is incorrect");

        var userInfo = await _carsharingContext.UserInfos.FirstOrDefaultAsync(x => x.UserId == user.Id);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
            new Claim(ClaimTypes.Email, dto.Email),
            new Claim(ClaimTypes.DateOfBirth, userInfo.BirthDay.ToString())
        };

        await _signInManager.SignInWithClaimsAsync(user, false, claims);

        return Ok();
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