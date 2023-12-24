using Microsoft.AspNetCore.Mvc;
using Minio;
using MinioConsumer.Models;
using MinioConsumer.Services;
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

builder.Services.AddSingleton<MinioClientFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IS3Service, S3Service>();
builder.Services.AddScoped<ITempS3Service, TempS3Service>();
builder.Services.AddScoped<ITempMetadataRepository<DocumentMetadata>, RedisMetadataRepository<DocumentMetadata>>();
builder.Services.AddScoped<IMetadataRepository<DocumentMetadata>, MongoDbMetadataRepository<DocumentMetadata>>();
builder.Services.AddScoped<OperationRepository>();
builder.Services.AddScoped<MetadataSaver<DocumentMetadata>>();
builder.Services.AddScoped<PrimaryStorageSaver<DocumentMetadata>>();
//builder.Services.AddScoped<IMetadataRepository, MetadataRepository>();
//builder.Services.AddScoped<IMetadataRepository, MetadataRepository>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
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

app.MapPost("test", ([FromServices] MetadataSaver<DocumentMetadata> p) => p.UploadFileAsync(Guid.NewGuid(), new DocumentMetadata(Guid.NewGuid(), default)));
app.Run();


public class MinioClientFactory
{
    private readonly MinioConfiguration _tempMinioConfiguration;

    private readonly MinioConfiguration _primaryMinioConfiguration;

    private readonly IHttpClientFactory _httpClientFactory;

    public MinioClientFactory(IServiceProvider services)
    {
        var section =  services.GetService<IConfiguration>().GetSection("Minio");
        _tempMinioConfiguration = new MinioConfiguration();
        _primaryMinioConfiguration = new MinioConfiguration();
        section.GetSection("Temp").Bind(_tempMinioConfiguration);
        section.GetSection("Prime").Bind(_primaryMinioConfiguration);
        _httpClientFactory = services.GetRequiredService<IHttpClientFactory>();

    }

    public IMinioClient CreateClient(bool forTempStorage)
    {
        var configuration = forTempStorage ? _tempMinioConfiguration : _primaryMinioConfiguration;

        return new MinioClient()
            .WithSSL(configuration.UseSSL)
            .WithEndpoint(configuration.Endpoint)
            .WithCredentials(configuration.AccessKey, configuration.SecretKey)
            .WithTimeout(configuration.Timeout)
            .WithHttpClient(_httpClientFactory.CreateClient())
            .Build();
    }
}

public class MinioConfiguration
{
    public string Endpoint { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public bool UseSSL { get; set; }

    public int Timeout { get; set; }
}
