using AutoMapper;
using Carsharing.Helpers;
using Carsharing.ViewModels;
using Carsharing.ViewModels.Admin.UserInfo;
using Carsharing.ViewModels.Profile;
using Contracts.UserInfo;
using Features.Users.Commands.ChangePassword;
using Features.Users.Commands.EditUser;
using Features.Users.Queries.GetPersonalInfo;
using Features.Users.Queries.GetProfileInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using UserInfoVM = Carsharing.ViewModels.Profile.UserInfoVM;

namespace Carsharing.Controllers;

[Route("Api/Account")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IBalanceService _balanceService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProfileController(IBalanceService balanceService, IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
        _balanceService = balanceService;
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var queryResult = await _mediator.Send(new GetProfileInfoQuery(User.GetId()));
        var info = queryResult.Value;
        return queryResult.IsSuccess
            ? new JsonResult(new ProfileInfoVM
            {
                UserInfo = new UserInfoVM
                {
                    Balance = info!.PersonalInfo!.Balance,
                    Email = info.PersonalInfo.Email,
                    FullName = $"{info.PersonalInfo.FirstName} {info.PersonalInfo.LastName}"
                },
                BookedCars = info!.CurrentlyBookedCars!.Select(x => new ProfileCarVM
                {
                    Name = x.Model,
                    IsOpened = x.IsOpened,
                    LicensePlate = x.LicensePlate,
                    ImageUrl = x.Image
                })
            })
            : BadRequest(queryResult.ErrorMessage);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVM change)
    {
        var commandResult =
            await _mediator.Send(new ChangePasswordCommand(User.GetId(), change!.OldPassword!, change!.Password!));
        var info = commandResult.Value;

        return commandResult.IsSuccess && info?.Success is true
            ? NoContent()
            : BadRequest(new { error = new { code = (int)ErrorCode.ServiceError, messages = info!.Errors } });
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> PersonalInfo()
    {
        var queryResult = await _mediator.Send(new GetPersonalInfoQuery(User.GetId()));
        var info = queryResult.Value;
        return queryResult.IsSuccess && info is not null
            ? new JsonResult(new PersonalInfoVM()
            {
                Email = info.Email,
                Passport = info.Passport,
                Surname = info.LastName,
                Phone = info.Phone,
                BirthDate = DateOnly.FromDateTime(info.BirthDate),
                DriverLicense = info.DriverLicense,
                FirstName = info.FirstName
            })
            : BadRequest(queryResult.ErrorMessage); //TODO: как правильнее вернуть ошибку 
    }

    [HttpPut("PersonalInfo/Edit")]
    public async Task<IActionResult> Edit([FromBody] EditUserVm userVm)
    {
        var commandResult = await _mediator.Send(new EditUserCommand(User.GetId(),
            _mapper.Map<EditUserVm, EditUserDto>(userVm)));

        return commandResult.IsSuccess
            ? new JsonResult(new { result = "Success" })
            : new BadRequestObjectResult(new
            {
                error = new
                {
                    code = (int)ErrorCode.ServiceError,
                    message = "Ошибка сохранения"
                }
            });
    }

    [HttpGet("increase")]
    public async Task<IActionResult> IncreaseBalance([FromQuery] decimal val)
    {
        var result = await _balanceService.IncreaseBalance(User.GetId(), val);

        if (result == "success")
            return new JsonResult(new
            {
                result = $"Success, your Balance increased on {val}"
            });

        return new JsonResult(new
        {
            result = "Не удалось пополнить баланс"
        });
    }

    [HttpGet("open/{licensePlate:required}")]
    public async Task<IActionResult> OpenCar([FromRoute] string licensePlate)
    {
        var queryResult = await _mediator.Send(new GetProfileInfoQuery(User.GetId()));
        var info = queryResult.Value;
        throw new NotImplementedException();
        //var result = await _carService.OpenCar(info!.CurrentlyBookedCars!.Select(x => x).First(x => x.LicensePlate == licensePlate).Id);

        //return new JsonResult(new
        //{
        //    result
        //});
    }

    [HttpGet("close/{licensePlate:required}")]
    public async Task<IActionResult> CloseCar([FromRoute] string licensePlate)
    {
        var queryResult = await _mediator.Send(new GetProfileInfoQuery(User.GetId()));
        var info = queryResult.Value;
        throw new NotImplementedException();
        //var result = await _carService.CloseCar(info!.CurrentlyBookedCars!.Select(x => x).First(x => x.LicensePlate == licensePlate).Id);

        //return new JsonResult(new
        //{
        //    result
        //});
    }
}