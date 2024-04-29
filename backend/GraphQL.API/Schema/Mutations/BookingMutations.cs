using System.Security.Claims;
using Carsharing.Helpers;
using Carsharing.ViewModels;
using Contracts;
using Features.CarBooking.Commands.BookCar;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace GraphQL.API.Schema.Mutations;

public partial class Mutations
{
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
			TariffId = bookingInfo.TariffId,
		}));
		
		return commandResult
			? true
			: throw new GraphQLException(new Error(commandResult.ErrorMessage ?? string.Empty));
	}
}