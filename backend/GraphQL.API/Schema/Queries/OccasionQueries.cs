using Features.Occasion;
using GraphQL.API.Helpers.Extensions;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.API.Schema.Queries;

public partial class Queries
{
	[Authorize]
	public async Task<Guid> GetMyOpenedOccasionAsync(
		[FromServices] IMediator mediator,
		[Service] IHttpContextAccessor httpContextAccessor) 
	{
		var userId = httpContextAccessor.GetUserId();

		var getMyOccasionQuery = new GetOpenedUserOccasionQuery(userId);

		var occasionQuery = await mediator.Send(getMyOccasionQuery);

		if (!occasionQuery) 
			throw new GraphQLException(occasionQuery.ErrorMessage!);
		
		if(occasionQuery.Value is null || occasionQuery.Value == Guid.Empty)
			throw new GraphQLException("Not found.");

		return occasionQuery.Value.Value;
	}
}