using Carsharing;
using Carsharing.ChatHub;
using Carsharing.Helpers;
using Carsharing.Helpers.Extensions.ServiceRegistration;
using Microsoft.AspNetCore.Mvc;
using MassTransit;

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

        options.AddPolicy("DevFrontEnds",
            builder =>
                builder.WithOrigins(mainFront, adminFront)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(origin => true)
        );
    });

    services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.HttpOnly = true;
    });
}

var app = builder.Build();

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

app.Run();
