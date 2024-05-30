using BalanceService;
using BalanceService.GrpcServices;
using BalanceService.Helpers.Extensions.ServiceRegistration;
using BalanceService.Infrastructure.Persistence;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDbContext<BalanceContext>(options =>
    options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<TransactionMemory>();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();

app.MapGrpcService<GrpcBalanceService>();
app.MapGrpcService<GrpcUserService>();

await TryMigrateDatabaseAsync(app);
app.Run();

static async Task TryMigrateDatabaseAsync(WebApplication app)
{
    try
    {
        await using var scope = app.Services.CreateAsyncScope();
        var sp = scope.ServiceProvider;

        await using var db = sp.GetRequiredService<BalanceContext>();

        await db.Database.MigrateAsync();
    }
    catch (Exception e)
    {
        app.Logger.LogError(e, "Error while migrating the database");
        Environment.Exit(-1);
    }
}