using System.Security.Claims;
using Contracts;
using Features.CarBooking.Commands.BookCar;
using HotChocolate.Authorization;
using GraphQL.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GraphQL.API.Schema.Mutations;

public partial class Mutations
{
	[Authorize]
	[GraphQLName("bookCar")]
	public async Task<bool> BookCar(
		BookingVM bookingInfo,
		[FromServices] IMediator mediator,
		ClaimsPrincipal claimsPrincipal)
	{
		var commandResult = await mediator.Send(new BookCarCommand(new RentCarDto()
		{
			PotentialRenterUserId = claimsPrincipal.GetId(),
			End = bookingInfo.EndDate,
			Start = bookingInfo.StartDate,
			CarId = bookingInfo.CarId,
		}));;
		
		return commandResult
			? true
			: throw new GraphQLException(new Error(commandResult.ErrorMessage ?? string.Empty));
	}
}