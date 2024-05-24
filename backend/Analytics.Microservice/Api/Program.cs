using Analytics.Microservice.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.ConfigureApp();

app.Run();
