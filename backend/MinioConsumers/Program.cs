using MinioConsumer.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddMinioSetUp(configuration);
services.AddRedisSetUp(configuration);
services.AddMongoSetUp(configuration);
services.AddAuthenticationAndAuthorization(configuration);
services.AddServices();
services.AddInfrastructure();

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
}

var app = builder.Build();

#region Use Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion


if (app.Environment.IsDevelopment())
{
    app.UseCors("DevFrontEnds");
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();