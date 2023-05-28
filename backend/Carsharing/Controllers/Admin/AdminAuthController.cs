﻿using AutoMapper;
using Carsharing.Forms;
using Domain.Entities;
using Domain;
using Carsharing.ViewModels.Admin.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Carsharing.Helpers;
using Carsharing.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Carsharing.Controllers.Admin
{
    [Route("api/admin/auth")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AdminAuthController
            (
            UserManager<User> userManager,
            SignInManager<User> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            
            await _signInManager.SignInAsync(user, false);
            var userRoles = await _userManager.GetRolesAsync(user);
            LoginAdminVM response = new LoginAdminVM() { Roles = userRoles };
            return new JsonResult(response);
        }

        [HttpGet("become")]
        public async Task<IActionResult> BecomeAdmin ()
        {
            var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            if (id == null) 
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) 
                return NotFound();

            await _userManager.AddToRoleAsync(user, "admin");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));


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
            return new JsonResult(new LoginAdminVM() { Roles = roles });
        }

        private object GetLoginError() => new { error = new ErrorsVM { Code = (int)ErrorCode.ServiceError, Messages = new[] { "Неверная почта или пароль." } } };

    }
}
