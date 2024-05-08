using Chat.Services;
using Features.History;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Shared;
using Migrations;
using Microsoft.EntityFrameworkCore;

namespace Chat.Helpers;

public static class WebApplicationExtensions
{
    public static void Configure(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGrpcService<ChatServiceGrpc>();
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

    public static async Task TryMigrateDatabaseAsync(this WebApplication app)
    {
        try
        {
            await using var scope = app.Services.CreateAsyncScope();
            var sp = scope.ServiceProvider;

            await using var db = sp.GetRequiredService<ChatServiceContext>();
            await db.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "Error while migrating the database");
            Environment.Exit(-1);
        }
    }
}
