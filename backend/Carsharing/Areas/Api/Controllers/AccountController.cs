using Carsharing.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Carsharing.Persistence.GoogleAPI;
using AutoMapper;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.Areas.Api.Controllers;

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

    public AccountController(
        CarsharingContext carsharingContext,
        UserManager<User> userManager,
        IMapper mapper,
        SignInManager<User> signInManager,
        IConfiguration configuration
        )
    {
        _mapper = mapper;
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _carsharingContext = carsharingContext;
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
            return BadRequest();

        _userInfo.UserId = user.Id;
        _carsharingContext.Update(_userInfo);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.DateOfBirth, _userInfo?.BirthDay.ToString() ?? ""),
            new Claim("Passport", _userInfo?.Passport ?? "")
        };



        var pr = await _signInManager.CreateUserPrincipalAsync(user);
        pr.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        var resultSignIn = await _signInManager.PasswordSignInAsync(user, dto.Password ?? "", false, false);
        if (resultSignIn.Succeeded == false)
            return Unauthorized("����� ��� ������ �������.");

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pr);

        await _carsharingContext.SaveChangesAsync();

        return Ok();
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return Unauthorized("����� ��� ������ �������.");

        var userInfo = await _carsharingContext.UserInfos.FirstOrDefaultAsync(x => x.UserId == user.Id);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.DateOfBirth, userInfo?.BirthDay.ToString() ?? ""),
            new Claim("Passport", userInfo?.Passport ?? "")
        };

        var pr = await _signInManager.CreateUserPrincipalAsync(user);
        pr.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

        var resultSignIn = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
        if ( resultSignIn.Succeeded == false)
            return Unauthorized("����� ��� ������ �������.");
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pr);

        return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("external-login")]
    public IActionResult ExternalLogin([FromForm] string provider, [FromForm] string returnUrl)
    {
        var redirectUrl = $"https://localhost:7129/api/account/external-auth-callback?returnUrl={returnUrl}";
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        properties.AllowRefresh = true;
        return Challenge(properties, provider);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("external-auth-callback")]
    public async Task<IActionResult> ExternalLoginCallback([FromQuery] string code, [FromQuery] string scope)
    {
        object locker = new object();
        try
        {
            var getTokenResult = await GoogleAPI.GetTokenAsync(code,
                _configuration["Authorization:Google:AppId"] ?? "",
                _configuration["Authorization:Google:AppSecret"] ?? "",
                "https://localhost:7129/api/account/external-auth-callback"
                );
            if (getTokenResult is null)  
                return StatusCode(500, "�� ���������� �������� ������ � ������� Google");

            var getUserResult = await GoogleAPI.GetUserAsync(tokenId: getTokenResult.id_token, accessToken: getTokenResult.access_token);

            if (getUserResult is null)
                return StatusCode(500, "�� ���������� �������� ������ � ������� Google");

            User user = _mapper.Map<User>(getUserResult);
            UserInfo userInfo = null;

            lock (locker)
            {
                if (_userManager.FindByEmailAsync(user.Email).GetAwaiter().GetResult() is null)
                {
                    ///TODO: �������� �������� ����� � �������� � ������ � ������� lock
                   
                    UserInfo _userInfo = _mapper.Map<UserInfo>(getUserResult);
                    var userInfoDb = _carsharingContext.UserInfos.AddAsync(_userInfo).GetAwaiter().GetResult();
                    user.UserInfo = userInfoDb.Entity;

                    var createUserResult = _userManager.CreateAsync(user).GetAwaiter().GetResult();
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

            HttpContext.Response.Cookies.Append(CookieAuthenticationDefaults.AuthenticationScheme, pr.ToString());

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pr);


            return Redirect("http://localhost:3000/profile");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Redirect("https://localhost:3000/login/");
        }

    }

    [HttpPost("[action]")]
    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }

}