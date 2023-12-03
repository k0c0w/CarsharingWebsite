using MassTransit;
using Minio;
using MinioConsumer.Services.PrimaryStorageSaver;
using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
        ConnectionMultiplexer.Connect(
            builder.Configuration["Redis:Connection"] ?? throw new InvalidOperationException())
);

builder.Services.AddMinio(configuration =>
{
    configuration.WithSSL(false);
    configuration.WithTimeout(int.Parse(builder.Configuration["MinioS3:Timeout"]!));
    configuration.WithEndpoint(builder.Configuration["MinioS3:Endpoint"]);
    configuration.WithCredentials(
        builder.Configuration["MinioS3:AccessKey"]!,
        builder.Configuration["MinioS3:SecretKey"]!);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IS3Service, S3Service>();
builder.Services.AddScoped<IMetadataRepository, MetadataRepository>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<IConsumer<SaveInPRimaryDbRequest>>(typeof(SaveInPRimaryDbRequestConsumer));
});
builder.Services.AddControllers();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();