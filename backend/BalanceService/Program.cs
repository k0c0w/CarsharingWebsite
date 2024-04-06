using BalanceMicroservice.Clients;
using BalanceService;
using BalanceService.GrpcServices;
using BalanceService.Helpers.Extensions.ServiceRegistration;
using BalanceService.Infrastructure.Persistence;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BalanceServiceClient = BalanceMicroservice.Clients.BalanceService.BalanceServiceClient;
using UserManagementServiceClient = BalanceMicroservice.Clients.UserManagementService.UserManagementServiceClient;

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

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.MapGet("/create_user", async () => 
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:7242");
        var client = new UserManagementServiceClient(channel);
        var request = new GrpcUserRequest() { Id = Guid.NewGuid().ToString() };
        var reply = await client.AddUserAsync(request);
        return new { Request = request, Result = reply };
    });
    
    app.MapGet("/prepare_transaction",
        async ([FromQuery] Guid guid, [FromQuery] long amount, [FromQuery] bool isPositive, [FromQuery] int fractionPart) =>
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7242");
            var client = new BalanceServiceClient(channel);
            
            var decimalVal = new DecimalValue()
            {
                IntegerPart = amount, IsPositive = isPositive, FractionPart = fractionPart
            };
            var request = new BalanceChangeRequest() { UserId = guid.ToString(), BalanceChange = decimalVal};
            var reply = await client.PrepareTransactionAsync(request);
            
            return new { Request = request, Result = reply };
        });
    
    app.MapGet("/prepare_wrong_transaction", 
        async ([FromQuery]long amount) => 
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7242");
            var client = new BalanceServiceClient(channel);
            
            var decimalVal = new DecimalValue()
            {
                IntegerPart = amount, IsPositive = true, FractionPart = 0
            };
            var request = new BalanceChangeRequest() { UserId = Guid.NewGuid().ToString(), BalanceChange = decimalVal};
            var reply = await client.PrepareTransactionAsync(request);
            
            return new { Request = request, Result = reply };
        });
    
    app.MapGet("/commit_transaction",
        async ([FromQuery] Guid guid) =>
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7242");
            var client = new BalanceServiceClient(channel);
            
            var request = new Transaction() { Id = guid.ToString() };
            var reply = await client.CommitTransactionAsync(request);
            
            return new { Request = request, Result = reply };
        });

    app.MapGet("/abort_transaction",
        async ([FromQuery] Guid guid, [FromQuery] long amount) =>
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7242");
            var client = new BalanceServiceClient(channel);
            
            var request = new Transaction() { Id = guid.ToString() };
            var reply = await client.AbortTransactionAsync(request);
            
            return new { Request = request, Result = reply };
        });
}

app.MapGrpcService<GrpcBalanceService>();
app.MapGrpcService<GrpcUserService>();
app.UseHttpsRedirection();

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