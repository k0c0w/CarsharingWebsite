using BalanceService.GrpcServices;
using BalanceService.Helpers.Extensions.ServiceRegistration;
using BalanceService.Infrastructure.Persistence;
using Contracts;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDbContext<BalanceContext>(options =>
    options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
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
        var client = new UserService.UserServiceClient(channel);
        var request = new GrpcUserRequest() { Id = Guid.NewGuid().ToString() };
        var reply = await client.AddUserAsync(request);
        return new { Request = request, Result = reply };
    });
    
    app.MapGet("/prepare_transaction",
        async ([FromQuery] Guid guid, [FromQuery] long amount) =>
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7242");
            var client = new Contracts.BalanceService.BalanceServiceClient(channel);
            var request = new BalanceRequest() { UserId = guid.ToString(), Value = amount };
            var reply = await client.PrepareTransactionAsync(request);
            return new { Request = request, Result = reply };
        });
    
    app.MapGet("/prepare_wrong_transaction", 
        async ([FromQuery]long amount) => 
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7242");
            var client = new Contracts.BalanceService.BalanceServiceClient(channel);
            var request = new BalanceRequest() { UserId = Guid.NewGuid().ToString(), Value = amount };
            var reply = await client.PrepareTransactionAsync(request);
            return new { Request = request, Result = reply };
        });
    
    app.MapGet("/commit_transaction",
        async ([FromQuery] Guid guid, [FromQuery] long amount) =>
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7242");
            var client = new Contracts.BalanceService.BalanceServiceClient(channel);
            var request = new BalanceRequest() { UserId = guid.ToString(), Value = amount };
            var reply = await client.CommitTransactionAsync(request);
            return new { Request = request, Result = reply };
        });

    app.MapGet("/abort_transaction",
        async ([FromQuery] Guid guid, [FromQuery] long amount) =>
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7242");
            var client = new Contracts.BalanceService.BalanceServiceClient(channel);
            var request = new BalanceRequest() { UserId = guid.ToString(), Value = amount };
            var reply = await client.AbortTransactionAsync(request);
            return new { Request = request, Result = reply };
        });
}

app.MapGrpcService<GrpcBalanceService>();
app.MapGrpcService<GrpcUserService>();
app.UseHttpsRedirection();

app.Run();