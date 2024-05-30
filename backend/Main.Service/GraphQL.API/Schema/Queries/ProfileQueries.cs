using System.Security.Claims;
using Features.Users.Queries.GetPersonalInfo;
using Features.Users.Queries.GetProfileInfo;
using GraphQL.API.ViewModels.Profile;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CommonExtensions.Claims;

namespace GraphQL.API.Schema.Queries;

public partial class Queries
{
	[Authorize]
	public async Task<ProfileInfoVM> GetProfile(
		[FromServices] IMediator mediator, 
		ClaimsPrincipal claimsPrincipal)
	{
		var userId = claimsPrincipal.GetId();
		var queryResult = await mediator.Send(new GetProfileInfoQuery(userId));
		var info = queryResult.Value;
		
		return queryResult.IsSuccess
			? new ProfileInfoVM
			{
				UserInfo = new UserInfoVM
				{
					Balance = info!.PersonalInfo!.Balance,
					Email = info.PersonalInfo.Email,
					FullName = $"{info.PersonalInfo.FirstName} {info.PersonalInfo.LastName}",
					Name = info.PersonalInfo.FirstName,
                    SecondName = info.PersonalInfo.LastName,
					BirthDate = info.PersonalInfo.BirthDate,
					IsConfirmed = info.PersonalInfo.Confirmed
                },
				BookedCars = info.CurrentlyBookedCars!.Select(x => new ProfileCarVM
				{
					Id = x.Id,
					Name = $"{x.Brand} {x.Model}",
					IsOpened = x.IsOpened,
					LicensePlate = x.LicensePlate,
					ImageUrl = x.ImageUrl
				})
			}
			: throw new GraphQLException(queryResult.ErrorMessage!);
	}
	
	[Authorize]
	public async Task<PersonalInfoVM> GetPersonalInfo(
		[FromServices] IMediator mediator, 
		ClaimsPrincipal claimsPrincipal)
	{
		var userId = claimsPrincipal.GetId();
		var queryResult = await mediator.Send(new GetPersonalInfoQuery(userId));
		var info = queryResult.Value;
		
		return queryResult.IsSuccess && info is not null
			? new PersonalInfoVM
			{
				Email = info.Email,
				Passport = info.Passport,
				Surname = info.LastName,
				Phone = info.Phone,
				BirthDate = DateOnly.FromDateTime(info.BirthDate),
				DriverLicense = info.DriverLicense,
				FirstName = info.FirstName
			}
			: throw new GraphQLException(queryResult.ErrorMessage!);
	}
}