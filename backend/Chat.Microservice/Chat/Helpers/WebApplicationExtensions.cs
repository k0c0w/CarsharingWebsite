using Chat.Services;
using Migrations;
using Microsoft.EntityFrameworkCore;
using Chat.GrpcImplementations;

namespace Chat.Helpers;

public static class WebApplicationExtensions
{
    public static void Configure(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGrpcService<ChatServiceGrpc>();
        app.MapGrpcService<ManagementServiceGrpc>();
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
