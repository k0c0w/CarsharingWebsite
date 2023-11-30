using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Minio;
using MinioConsumers.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMinio(configuration =>
{
    configuration.WithSSL(false);
    configuration.WithTimeout(int.Parse(builder.Configuration["MinioS3:Timeout"]!));
    configuration.WithEndpoint(builder.Configuration["MinioS3:Endpoint"]);
    configuration.WithCredentials(
        builder.Configuration["MinioS3:AccessKey"]!,
        builder.Configuration["MinioS3:SecretKey"]!);
});

builder.Services.AddScoped<IS3Service, S3Service>();

var app = builder.Build();


app.MapGet("files/{bucketName}/{fileName}", async ([FromRoute] string bucketName, [FromRoute] string fileName, [FromServices] IS3Service storageService) =>
{
    var bucketExists = await storageService.BucketExsistAsync(bucketName);

    if (!bucketExists)
        return Results.NotFound(default);

    var file = await storageService.GetFileFromBucketAsync(fileName, bucketName);

    return file is null ? Results.NotFound(default) : Results.Stream(file.ContentStream, contentType: file.ContentType.ToString());
});


app.Run();