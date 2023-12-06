using MinioConsumer.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddMinioSetUp(configuration);
services.AddRedisSetUp(configuration);
services.AddMongoSetUp(configuration);
services.AddServices();
services.AddInfrastructure();

var app = builder.Build();

#region Use Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();