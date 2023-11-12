using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Carsharing.Helpers;
using Carsharing.ViewModels.Admin.UserInfo;
using Features.Balance.Commands.DecreaseBalance;
using Features.Balance.Commands.IncreaseBalance;
using Features.Users.Commands.EditUser;
using Features.Users.Queries.GetAllInfo;
using Features.Users.Queries.GetUserInfoById;
using Features.Users.Queries.Verify;
using MediatR;
using Services.Abstractions;
using EditUserDto = Contracts.UserInfo.EditUserDto;

namespace Carsharing.Controllers;

[ApiController]
[Route("/api/admin/user")]
public class AdminUserController : ControllerBase
{
    private readonly RoleManager<UserRole> _roleManager;

    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AdminUserController(UserManager<User> userManager, RoleManager<UserRole> roleManager, IMediator mediator,
        IMapper mapper)
    {
        _roleManager = roleManager;
        _mediator = mediator;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet("all")]
    public async Task<IActionResult> All()
    {
        var queryResult = await _mediator.Send(new GetAllInfoQuery());
        if (!queryResult.IsSuccess || queryResult.Value is null)
            return BadRequest();

        return new JsonResult(queryResult.Value.Select(x => new UserVM
            {
                Email = x.User.Email!,
                Id = x.UserId!,
                EmailConfirmed = x.User.EmailConfirmed,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                PersonalInfo = new UserInfoVM
                {
                    Balance = x.Balance,
                    Passport = x.PassportType != null ? $"{x.PassportType} {x.Passport}" : null,
                    Verified = x.Verified,
                    BirthDay = DateOnly.FromDateTime(x.BirthDay),
                    DriverLicense = x.DriverLicense
                }
            })
        );
    }

    [HttpGet("{id:required}")]
    public async Task<IActionResult> GetUser([FromRoute] string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();
        var result = await _mediator.Send(new GetUserInfoByIdQuery(id));
        var user = result.Value;
        if (result.IsSuccess is false || user == null)
            return NotFound(ServiceError("No such user"));

        var userVm = _mapper.Map<UserInfo, UserVM>(user);
        return new JsonResult(userVm);
    }

    [HttpPut("Edit/{id:required}")]
    public async Task<IActionResult> EditUser([FromRoute] string id, [FromBody] EditUserVM edit)
    {
        if (string.IsNullOrEmpty(id)) return NotFound(ServiceError("No such user"));

        var commandResult = await _mediator.Send(new EditUserCommand(id,
            _mapper.Map<EditUserVM, EditUserDto>(edit)));

        return commandResult.IsSuccess
            ? NoContent()
            : new JsonResult(ServiceError("Часть информации не сохранена"));
    }

    [HttpPut("verify/{id:required}")]
    public async Task<IActionResult> VerifyUserChanges([FromRoute] string id)
    {
        var result = await _mediator.Send(new VerifyQuery(id));

        return result.IsSuccess
            ? new JsonResult(new { result = "Success" })
            : new JsonResult(new { result = "Fail", Message = result.ErrorMessage });
    }

    [HttpPost("{id:required}/BalanceIncrease")]
    public async Task<IActionResult> IncreaseBalance([FromRoute] string id, [FromBody] decimal val)
    {
        if (val <= 0) return BadRequest();
        var commandResult = await _mediator.Send(new IncreaseBalanceCommand(id, val));

        return commandResult.IsSuccess
            ? new JsonResult(new
            {
                result = $"Success, your Balance increased on {val}"
            })
            : new JsonResult(new
            {
                result = "Не удалось пополнить баланс"
            });
    }

    [HttpPost("{id:required}/BalanceDecrease")]
    public async Task<IActionResult> DecreaseBalance([FromRoute] string id, [FromBody] decimal val)
    {
        if (val <= 0) return BadRequest();
        var commandResult = await _mediator.Send(new DecreaseBalanceCommand(id, val));


        return commandResult.IsSuccess
            ? new JsonResult(new
            {
                result = $"Success, your Balance increased on {val}"
            })
            : new JsonResult(new
            {
                result = "Не удалось пополнить баланс"
            });
    }

    [HttpPost("{id:required}/GrantRole/{role:required}")]
    public async Task<IActionResult> GrantRole([FromRoute] string role, [FromRoute] string id)
    {
        //нужно будет добавить ограничения кто кому может менять роль
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound(ServiceError("No such user"));

        var newUserRole = await _roleManager.FindByNameAsync(role);
        if (newUserRole == null) return NotFound(ServiceError("No such role"));

        var userRole = await _userManager.GetRolesAsync(user);

        if (userRole.Contains(newUserRole!.Name!))
            return Ok();

        await _userManager.AddToRoleAsync(user, newUserRole.Name!);

        return Ok();
    }

    [HttpDelete("{id:required}/RevokeRole/{role:required}")]
    public async Task<IActionResult> RevokeRole([FromRoute] string role, [FromRoute] string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound(ServiceError("No such user"));

        var newUserRole = await _roleManager.FindByNameAsync(role);
        if (newUserRole == null) return NotFound(ServiceError("No such role"));

        var userRole = await _userManager.GetRolesAsync(user);
        if (!userRole!.Contains(newUserRole!.Name!))
            return NoContent();

        await _userManager.RemoveFromRoleAsync(user, newUserRole!.Name!);

        return NoContent();
    }

    private static object ServiceError(string message)
    {
        return new { error = new { code = (int)ErrorCode.ServiceError, message = message } };
    }
}