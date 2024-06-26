﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Minio;
using MinioConsumer.DependencyInjection.ConfigSettings;
using MinioConsumer.Models;
using MinioConsumer.Services.PrimaryStorageSaver;
using MinioConsumer.Services;
using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;
using MongoDB.Driver;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using MinioConsumer.BackgroundServices;
using CommonExtensions.Jwt;
using CommonExtensions.Authorization;
using CommonExtensions;

namespace MinioConsumer.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddMinioSetUp(this IServiceCollection services, IConfiguration builderConfiguration)
    {
        services.AddMinio(configuration =>
        {
            configuration.WithSSL(false);
            configuration.WithTimeout(int.Parse(builderConfiguration["MinioS3:Timeout"]!));
            configuration.WithEndpoint(builderConfiguration["MinioS3:Endpoint"]);
            configuration.WithCredentials(
                builderConfiguration["MinioS3:AccessKey"]!,
                builderConfiguration["MinioS3:SecretKey"]!);
        });

        services.AddScoped<IS3Service, S3Service>();
    }

    public static void AddRedisSetUp(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisDbSettings>(configuration.GetSection(nameof(RedisDbSettings)));

        services.AddSingleton<IConnectionMultiplexer>(x =>
        {
            var settings = new RedisDbSettings();
            configuration.GetSection(nameof(RedisDbSettings)).Bind(settings);

            return ConnectionMultiplexer.Connect(settings.ConnectionUrl);
        });

        services.AddScoped<ITempMetadataRepository<DocumentMetadata>, RedisMetadataRepository<DocumentMetadata>>();
        services.AddScoped<ITempMetadataRepository<OccasionAttachmentMetadata>, RedisMetadataRepository<OccasionAttachmentMetadata>>();
        services.AddScoped<OperationRepository>();
    }

    public static void AddMongoSetUp(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = new MongoDbSettings();
            configuration.GetSection(nameof(MongoDbSettings)).Bind(settings);

            return new MongoClient(settings.ConnectionUrl);
        });

        services.AddScoped<IMetadataRepository<DocumentMetadata>, DocumentMetadataRepository>();
        services.AddScoped<IMetadataRepository<OccasionAttachmentMetadata>, OccasionAttachmentMetadataRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<MetadataSaver<DocumentMetadata>>();
        services.AddScoped<MetadataSaver<OccasionAttachmentMetadata>>();
        services.AddScoped<PrimaryStorageSaver<DocumentMetadata>>();
        services.AddScoped<PrimaryStorageSaver<OccasionAttachmentMetadata>>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
        });
    }

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

        services.AddControllers();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddAuthenticationAndAuthorization(configuration);
    }

    internal static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.Configure<JwtSettings>(
            configuration.GetSection(JwtSettings.Jwt));

        serviceCollection
            .AddConfiguredAuthentication()
            .AddJwtBearer(configuration);

        serviceCollection.AddAuthorization();

        return serviceCollection;
    }

    internal static void AddBackgroundWorkers(this IServiceCollection services)
    {
        services.AddHostedService<TempFileCleanerBackgroundService>();
        services.AddHostedService<BackgroundSaver>();
        services.AddScoped<IMetadataScopedProcessingService, TempFileCleanerScopedProcessingService>();
        services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
    }
}
