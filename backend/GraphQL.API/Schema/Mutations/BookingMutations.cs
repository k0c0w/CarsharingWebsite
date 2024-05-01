using Carsharing.ViewModels;
using Contracts;
using Features.CarBooking.Commands.BookCar;
using GraphQL.API.Helpers.Extensions;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.API.Schema.Mutations;

public partial class Mutations
{
	[Authorize]
	[GraphQLName("bookCar")]
	public async Task<bool> BookCar(
		BookingVM bookingInfo,
		[FromServices] IMediator mediator,
		[Service] IHttpContextAccessor httpContextAccessor)
	{
		var commandResult = await mediator.Send(new BookCarCommand(new RentCarDto()
		{
			PotentialRenterUserId = httpContextAccessor.GetUserId(),
			End = bookingInfo.EndDate,
			Start = bookingInfo.StartDate,
			CarId = bookingInfo.CarId,
			TariffId = bookingInfo.TariffId,
		}));;
		
		return commandResult
			? true
			: throw new GraphQLException(new Error(commandResult.ErrorMessage ?? string.Empty));
	}
}