using Chat.Services;
using Features.History;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Shared;

namespace Chat.Helpers;

public static class WebApplicationExtensions
{
    public static async Task ConfigureAsync(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGrpcService<ChatServiceImplementaion>();
        app.MapGet("/history/{topic:string}", async Task<Results<JsonHttpResult<IEnumerable<MessageDto>>, NotFound>> (
            [FromRoute] string topic,
            [FromServices] IMediator mediator,
            [FromBody] ClaimsPrincipal userClaims,
            [FromQuery] int limit = 64,
            [FromQuery] int offset = 0
            ) =>
        {
            var query = new GetHistoryQuery(userClaims.GetId(), topic, limit, offset);

            var queryResult = await mediator.Send(query);
            return queryResult.IsSuccess ? TypedResults.Json(queryResult.Value) : TypedResults.NotFound();
        });

    }
}
