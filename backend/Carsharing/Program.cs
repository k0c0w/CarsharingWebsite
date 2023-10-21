using Carsharing;
using Carsharing.ChatHub;
using Carsharing.Helpers;
using Carsharing.Helpers.Extensions.ServiceRegistration;
using Carsharing.Helpers.Options;
using Microsoft.AspNetCore.Mvc;
using Domain.Common;
using MassTransit;
using Microsoft.Extensions.Options;
using Migrations.CarsharingApp;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.Configure<RabbitMqOption>(builder.Configuration.GetSection(RabbitMqOption.RabbitMq));
services.AddSingleton(sp => sp.GetRequiredService<IOptions<RabbitMqOption>>().Value);

services.AddDatabase(builder.Configuration)
        .AddIdentityAuthorization()
        .AddControllers();

services.AddAutoMapper(typeof(Program).Assembly)
        .RegisterSwagger();

services.RegisterChat()
        .RegisterBuisnessLogicServices();

services.AddMassTransit(busConfig =>
{
    busConfig.SetKebabCaseEndpointNameFormatter();
    busConfig.AddConsumer<ChatMessageConsumer>();
    busConfig.UsingRabbitMq((context, cfg) =>
    {
        RabbitMqOption options = context.GetRequiredService<RabbitMqOption>(); // ??????? ????? option ???????? ??????, ?? ?? ??????????
        cfg.ConfigureEndpoints(context);
        cfg.Host(new Uri(config["RabbitMq:Host"]!), c => {
            c.Username(config["RabbitMq:Username"]!);
            c.Password(config["RabbitMq:Password"]!);
        });
    });
});
services.AddTransient<IMessageProducer, MessageProducer>();

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

builder.Services.AddScoped<IMessageProducer, MessageProducer>();

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
