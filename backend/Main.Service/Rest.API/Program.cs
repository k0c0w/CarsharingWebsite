using Carsharing.ChatHub;
using Carsharing.Helpers;
using Carsharing.Helpers.Extensions.ServiceRegistration;
using Microsoft.AspNetCore.Mvc;
using MassTransit;
using Migrations.CarsharingApp;
using Carsharing.HostedServices;
using ApiExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var currentAssembly = Assembly.GetExecutingAssembly();

services.AddDatabase(builder.Configuration)
        .AddMassTransitWithRabbitMQProvider(builder.Configuration, currentAssembly);

services.AddIdentityAuthorization()
        .AddControllers();

services.AddAutoMapper(currentAssembly);

services.RegisterChat()
        .RegisterBusinessLogicServices(builder.Configuration)
        .AddMediatorWithFeatures()
        .RegisterSwagger();

services.AddAuthenticationAndAuthorization(builder.Configuration, () =>
{
    return new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // если запрос направлен хабу
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/occasion_chat"))
            {
                // получаем токен из строки запроса
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

services.Configure<ApiBehaviorOptions>(o =>
{
    o.InvalidModelStateResponseFactory = actionContext =>
    {
        var modelState = actionContext.ModelState;
        var json = modelState.Keys
            .ToDictionary(x => x, x => modelState[x]!.Errors.Select(x => x.ErrorMessage));

        return new BadRequestObjectResult(new
        { error = new { code = ErrorCode.ViewModelError, errors = json } });
    };
});

services.AddHostedService<MigrateDatabaseBackgroundService<CarsharingContext>>();

services.AddCors(options =>
{
    var configuration = builder.Configuration;
    var mainFront = configuration["KnownHosts:FrontendHosts:Main"]!;
    var adminFront = configuration["KnownHosts:FrontendHosts:Admin"]!;

    options.AddPolicy("DevFrontEnds",
        builder =>
            builder.WithOrigins(mainFront, adminFront)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(origin => true)
    );
});


var app = builder.Build();

app.UseSwagger()
   .UseSwaggerUI()
   .UseCors("DevFrontEnds");

app.UseAuthentication()
   .UseAuthorization();

app.MapControllers();
app.MapHub<OccasionsSupportChatHub>("/occasion_chat");

app.Run();
