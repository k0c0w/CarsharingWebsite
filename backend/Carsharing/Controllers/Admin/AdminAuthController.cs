using Carsharing.Forms;
using Domain.Entities;
using Carsharing.ViewModels.Admin.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Carsharing.Helpers;
using Carsharing.ViewModels;
using System.Security.Claims;
using Carsharing.Helpers.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Carsharing.Controllers.Admin
{
    [Route("api/admin/auth")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public AdminAuthController
            (
            UserManager<User> userManager,
            SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
                return Unauthorized(GetLoginError());

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
            if (!result.Succeeded)
                return Unauthorized(GetLoginError());

            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var token = _jwtGenerator.CreateToken(claims: claims, user: user);
            var response = new LoginAdminVM(userRoles, token);
            return new JsonResult(response);
        }

        [AllowAnonymous]
        [HttpGet("become")]
        public async Task<IActionResult> BecomeAdmin ()
        {
            var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            if (id == null) 
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) 
                return NotFound();

            await _userManager.AddToRoleAsync(user, Role.Admin.ToString());
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, Role.Admin.ToString()));
            
            await _signInManager.SignInAsync(user, false);
            
            return Ok();
        }

        [HttpGet("IsAdmin")]
        // add policy with manager
        public IActionResult IsAdmin()
        {
            IEnumerable<string> roles = User.Claims.Where(claim => claim.Type == ClaimTypes.Role)
                .Select(claim => claim.Value)
                .ToList();
            var jwt = "";
            if (Request.Headers.TryGetValue("Authorization", out var headerValue))
                jwt = headerValue.ToString().Replace("Bearer ", "").Trim();
            
            return new JsonResult(new LoginAdminVM( roles, jwt ));
        }

        private static object GetLoginError() => new { error = new ErrorsVM { Code = (int)ErrorCode.ServiceError, Messages = new[] { "Неверная почта или пароль." } } };

    }
}
