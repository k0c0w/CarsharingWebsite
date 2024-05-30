using System.Security.Claims;
using Features.Occasion;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CommonExtensions.Claims;

namespace GraphQL.API.Schema.Queries;

public partial class Queries
{
	[Authorize]
	public async Task<Guid> GetMyOpenedOccasionAsync(
		[FromServices] IMediator mediator,
		ClaimsPrincipal claimsPrincipal) 
	{
		var userId = claimsPrincipal.GetId();

		var getMyOccasionQuery = new GetOpenedUserOccasionQuery(userId);

		var occasionQuery = await mediator.Send(getMyOccasionQuery);

		if (!occasionQuery) 
			throw new GraphQLException(occasionQuery.ErrorMessage!);
		
		if(occasionQuery.Value is null || occasionQuery.Value == Guid.Empty)
			throw new GraphQLException("Not found.");

		return occasionQuery.Value.Value;
	}
}