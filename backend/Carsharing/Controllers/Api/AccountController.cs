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
using Carsharing.Helpers.Authorization;
using Migrations.CarsharingApp;
using Domain.Common;
using Persistence.Chat.ChatEntites.Dtos;
using Shared.Results;

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
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public AccountController(
        CarsharingContext carsharingContext,
        UserManager<User> userManager,
        IMapper mapper,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        IJwtGenerator jwtGenerator)
    {
        _mapper = mapper;
        _configuration = configuration;
        _jwtGenerator = jwtGenerator;
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

        var user = new User { Email = vm.Email, LastName = vm.Surname, FirstName = vm.Name, UserName = $"{DateTime.Now:MMddyyyyHHssmm}"};
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
        await _userManager.AddToRoleAsync(user, Role.User.ToString());
        // await _signInManager.SignInAsync(user, false);
        var claims = await _userManager.GetClaimsAsync(user);
        var token = _jwtGenerator.CreateToken(user: user, claims: claims);
        
        return new JsonResult(new ResponseBearerTokenVm(token));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginVM vm)
    {
        var user = await _userManager.FindByEmailAsync(vm.Email);
        if (user == null)
            return Unauthorized(GetLoginError());

        var resultSignIn = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
        if (!resultSignIn.Succeeded)
            return Unauthorized(GetLoginError());

        var claims = await _userManager.GetClaimsAsync(user);
        var token = _jwtGenerator.CreateToken(user: user, claims: claims);
        
        return new CreatedResult("/", new ResponseBearerTokenVm(token));
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("google-external-auth-callback")]
    public async Task<IActionResult> GoogleExternalLoginCallback([FromQuery] string code)
    {
        try
        {
            var getTokenResult = await GoogleApi.GetTokenAsync(code,
                _configuration["Authorization:Google:AppId"] ?? "",
                _configuration["Authorization:Google:AppSecret"] ?? "",
                _configuration["Authorization:Google:ReturnUri"]!
                );
            if (getTokenResult is null)
                return BadRequest(GetGoogleError());
            var getUserResult = await GoogleApi.GetUserAsync(tokenId: getTokenResult.id_token, accessToken: getTokenResult.access_token);

            if (getUserResult is null)
                return BadRequest(GetGoogleError());
                
            var user = _mapper.Map<User>(getUserResult);
            UserInfo? userInfo = default;
            
            if (await _userManager.FindByEmailAsync(user!.Email!) is null)
            {
                var _userInfo = _mapper.Map<UserInfo>(getUserResult);
                var userInfoDb = await _carsharingContext.UserInfos.AddAsync(_userInfo);
                user.UserInfo = userInfoDb.Entity;
                user.UserName = $"{DateTime.Now:MMddyyyyHHssmm}";
                

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
            
            userInfo ??= await _carsharingContext.UserInfos.SingleAsync(entity => entity.UserId == user.Id);

            var claims = await _userManager.GetClaimsAsync(user);
            claims.Add(new Claim(ClaimTypes.DateOfBirth, "1992-04-19 11:25:07.53+04"));
            claims.Add(new Claim("Passport", userInfo.Passport ?? "passport"));
            var token = _jwtGenerator.CreateToken(user, claims);

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
    
    private static object GetLoginError() => new { error = new ErrorsVM{ Code = (int)ErrorCode.ServiceError, Messages = new [] {"Неверная почта или пароль."} } };

    private static object GetGoogleError() => new
    {
        error = new ErrorsVM
        {
            Code = (int)ErrorCode.ExternalError,
            Messages = new[] { "Не получилось получить доступ к сервису Google" }
        }
    };
}