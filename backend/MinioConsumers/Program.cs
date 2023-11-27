using MinioConsumers.Options;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.Exceptions;
using MinioConsumers;
using MinioConsumers.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<MinioConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.ConfigureEndpoints(ctx);
        cfg.Host(builder.Configuration
            .GetSection(nameof(RabbitMqConfig))
            .Get<RabbitMqConfig>()!
            .FullHostname);
    });
});

builder.Services.AddMinio(configuration =>
{
    configuration.WithSSL(false);
    configuration.WithTimeout(int.Parse(builder.Configuration["MinioS3:Timeout"]!));
    configuration.WithEndpoint(builder.Configuration["MinioS3:Endpoint"]);
    configuration.WithCredentials(
        builder.Configuration["MinioS3:AccessKey"]!,
        builder.Configuration["MinioS3:SecretKey"]!);
});

builder.Services.AddSingleton<IS3Service, S3Service>();

var app = builder.Build();


app.MapGet("api/files/{bucketName}/{fileName}", async (string bucketName, string fileName, IS3Service storageService) =>
{
    var stream = await storageService.GetFileFromBucketAsync(fileName, bucketName);

    return stream is not null ? Results.Stream(stream) : Results.NotFound();
});


app.Run();