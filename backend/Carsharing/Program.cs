using Carsharing;
using Carsharing.ChatHub;
using Carsharing.Helpers;
using Carsharing.Helpers.Extensions.ServiceRegistration;
using Domain.Common;
using Features.Utils;
using Microsoft.AspNetCore.Mvc;
using MassTransit;
using Migrations.CarsharingApp;
using Microsoft.EntityFrameworkCore;

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

services.AddTransient<IFileProducer, FileProducer>();

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

if (builder.Environment.IsDevelopment())
{
    services.AddCors(options =>
    {
        var configuration = builder.Configuration;
        var mainFront = configuration["FrontendHost:Main"]!;
        var adminFront = configuration["FrontendHost:Admin"]!;
        Console.WriteLine(configuration["FrontendHost:Admin"]);

        options.AddPolicy("DevFrontEnds",
            builder =>
                builder.WithOrigins(mainFront, adminFront)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(origin => true)
        );
    });
}

var app = builder.Build(); 
var migrateDatabaseTask = TryMigrateDatabaseAsync(app);

app.UseHttpsRedirection()
   .UseStaticFiles()
   .UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
       .UseSwaggerUI()
       .UseCors("DevFrontEnds");
}

app.UseAuthentication()
   .UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chat");


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