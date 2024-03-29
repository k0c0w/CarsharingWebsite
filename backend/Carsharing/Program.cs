using Carsharing;
using Carsharing.ChatHub;
using Carsharing.Helpers;
using Carsharing.Helpers.Extensions.ServiceRegistration;
using Microsoft.AspNetCore.Mvc;
using MassTransit;
using Migrations.CarsharingApp;
using Microsoft.EntityFrameworkCore;
using Persistence.Chat.ChatEntites.SignalRModels;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDatabase(builder.Configuration)
        .AddMassTransitWithRabbitMQProvider(builder.Configuration);

services.AddIdentityAuthorization()
        .AddControllers();

services.AddAutoMapper(typeof(Program).Assembly);

services.RegisterChat()
        .RegisterBuisnessLogicServices()
        .RegisterSwagger();

services.AddMediatorWithFeatures();

services.AddAuthenticationAndAuthorization(builder.Configuration);

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
var migrateDatabaseTask = TryMigrateDatabaseAsync(app);



    app.UseSwagger()
       .UseSwaggerUI()
       .UseCors("DevFrontEnds");

app.UseHttpsRedirection();
app.UseAuthentication()
   .UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.MapHub<OccasionsSupportChatHub>("/occasion_chat");


await migrateDatabaseTask;
app.Run();



static async Task TryMigrateDatabaseAsync(WebApplication app)
{
    try
    {
        await using var scope = app.Services.CreateAsyncScope();
        var sp = scope.ServiceProvider;

        await using var db = sp.GetRequiredService<CarsharingContext>();

        await db.Database.MigrateAsync();
    }
    catch (Exception e)
    {
        app.Logger.LogError(e, "Error while migrating the database");
        Environment.Exit(-1);
    }

}