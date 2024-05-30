using MinioConsumer.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddMinioSetUp(configuration);
services.AddRedisSetUp(configuration);
services.AddMongoSetUp(configuration);
services.AddServices();
services.AddInfrastructure(configuration);
services.AddBackgroundWorkers();

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

#region Use Swagger
app.UseSwagger();
app.UseSwaggerUI();
#endregion


app.UseCors("DevFrontEnds");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();