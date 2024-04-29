using System.Security.Claims;
using AutoMapper;
using Carsharing.ViewModels;
using Carsharing.ViewModels.Profile;
using Contracts.UserInfo;
using Features.Balance.Commands.IncreaseBalance;
using Features.CarManagement;
using Features.Users.Commands.ChangePassword;
using Features.Users.Commands.EditUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GraphQL.API.Schema.Mutations;

public partial class Mutations
{
	[Authorize]
	[GraphQLName("increaseBalance")]
	public async Task<string> IncreaseBalance(decimal val, 
		[FromServices] IMediator mediator, 
		ClaimsPrincipal claimsPrincipal)
	{
		var commandResult = await mediator.Send(new IncreaseBalanceCommand(claimsPrincipal.GetId(), val));

		return commandResult.IsSuccess
			? $"Success, your Balance increased on {val}"
			: throw new GraphQLException("Не удалось пополнить баланс");
	}

	[Authorize]
	[GraphQLName("openCar")]
	public async Task<bool> OpenCar(string licensePlate,
		[FromServices] IMediator mediator)
	{
		var openResult = await mediator.Send(new OpenCarCommand(licensePlate));

		return openResult.IsSuccess;
	}

	[Authorize]
	[GraphQLName("closeCar")]
	public async Task<bool> CloseCar(string licensePlate,
		[FromServices] IMediator mediator)
	{
		var closeResult = await mediator.Send(new CloseCarCommand(licensePlate));

		return closeResult.IsSuccess;
	}
	
	[Authorize]
	[GraphQLName("editProfile")]
	public async Task<bool> EditProfile(EditUserVm userVm,
		[FromServices] IMediator mediator, 
		[FromServices] IMapper mapper,
		ClaimsPrincipal claimsPrincipal)
	{
		Console.WriteLine("Попал в edit");
		var commandResult = await mediator.Send(new EditUserCommand(claimsPrincipal.GetId(),
			mapper.Map<EditUserVm, EditUserDto>(userVm)));

		return commandResult.IsSuccess
			? true
			: throw new GraphQLException(new Error ("Ошибка сохранения"));
	}
	
	[Authorize]
	[GraphQLName("changePassword")]
	public async Task<bool> ChangePassword(
		ChangePasswordVM change,
		[FromServices] IMediator mediator, 
		ClaimsPrincipal claimsPrincipal
		)
	{
		var commandResult =
			await mediator.Send(new ChangePasswordCommand(claimsPrincipal.GetId(), change!.OldPassword!, change!.Password!));
		var info = commandResult.Value;

		return commandResult.IsSuccess && info?.Success is true
			? true
			: throw new GraphQLException(new Error(info!.Errors
				.Aggregate((now, previous) => $"{now}; {previous}")));
	}
}