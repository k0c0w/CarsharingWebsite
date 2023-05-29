using Carsharing.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Carsharing.Persistence.GoogleAPI;
using Carsharing.ViewModels;
using Domain.Entities;
using Domain;
using Carsharing.Helpers;


namespace Carsharing.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
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
    public async Task<IActionResult> RegisterUser(RegistrationVm vm)
    {

        var client = await _userManager.FindByEmailAsync(vm.Email);
        if (client != null)
            return BadRequest(new {error=new ErrorsVM{ Code = (int)ErrorCode.ServiceError, Messages = new [] {"Не возможно создать пользователя."}}});

        var user = new User { Email = vm.Email, LastName = vm.Surname, FirstName = vm.Name, UserName = $"{DateTime.Now.ToString("MMddyyyyHHssmm")}"};
        var resultUserCreate = await _userManager.CreateAsync(user, vm.Password);

        if (!resultUserCreate.Succeeded)
            return BadRequest( new {error= new ErrorsVM
                {
                    Code = (int)ErrorCode.ServiceError,
                    Messages = resultUserCreate.Errors.Select(x => x.Description)
                }});

        var userInfo = new UserInfo { BirthDay = vm.Birthdate, UserId = user.Id};
        await _carsharingContext.UserInfos.AddAsync(userInfo);
        await _carsharingContext.SaveChangesAsync();
        
        var principal = await GetClaimsPrincipal(userInfo, user);
        await _signInManager.SignInAsync(user, false);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        
        return Created("/", null);
    }
    
    //todo: csrf secure token
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginVM vm)
    {
        var user = await _userManager.FindByEmailAsync(vm.Email);
        if (user == null)
            return Unauthorized(GetLoginError());

        var resultSignIn = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
        if (!resultSignIn.Succeeded)
            return Unauthorized(GetLoginError());

        var userInfo = await _carsharingContext.UserInfos.FirstOrDefaultAsync(x => x.UserId == user.Id);
        var principal = await GetClaimsPrincipal(userInfo, user);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

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
                _configuration["Authorization:Google:ReturnUri"]
                );
            if (getTokenResult is null)
                return BadRequest(GetGoogleError());
            var getUserResult = await GoogleAPI.GetUserAsync(tokenId: getTokenResult.id_token, accessToken: getTokenResult.access_token);

            if (getUserResult is null)
                return BadRequest(GetGoogleError());

            var user = _mapper.Map<User>(getUserResult);
            UserInfo userInfo = null;
            
            if (await _userManager.FindByEmailAsync(user.Email) is null)
            {
                ///TODO: выделить создание юзера и юзеринфо в сервис и сделать lock
                
                var _userInfo = _mapper.Map<UserInfo>(getUserResult);
                var userInfoDb = await _carsharingContext.UserInfos.AddAsync(_userInfo);
                user.UserInfo = userInfoDb.Entity;
                user.UserName = $"{DateTime.Now.ToString("MMddyyyyHHssmm")}";
                

                var createUserResult = await _userManager.CreateAsync(user);
                if (createUserResult.Succeeded)
                {
                    userInfoDb.Entity.UserId = user.Id;
                    userInfo = _carsharingContext.UserInfos.Update(userInfoDb.Entity).Entity;
                }
                else
                    return BadRequest(new{error=createUserResult.Errors});
                
                await _carsharingContext.SaveChangesAsync();
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

            return Redirect("/profile");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Redirect("/login");
        }
    }
    
    [HttpPost("logout")]
    public async Task LogOut()
    {
        await _signInManager.SignOutAsync();
    }



    [HttpGet("IsAuthorized")]
    [Authorize]
    public IActionResult UserIsAuthorized() => Ok();
    
    private object GetLoginError() => new { error = new ErrorsVM{ Code = (int)ErrorCode.ServiceError, Messages = new [] {"Неверная почта или пароль."} } };

    private object GetGoogleError() => new
    {
        error = new ErrorsVM
        {
            Code = (int)ErrorCode.ExternalError,
            Messages = new[] { "Не получилось получить доступ к сервису Google" }
        }
    };

    private async Task<ClaimsPrincipal> GetClaimsPrincipal(UserInfo userInfo, User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new (ClaimTypes.DateOfBirth, userInfo?.BirthDay.ToString() ?? ""),
            new ("Passport", userInfo?.Passport ?? "")
        };

        var pr = await _signInManager.CreateUserPrincipalAsync(user);
        pr.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
        return pr;
    }
}