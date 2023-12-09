using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Minio;
using MinioConsumer.DependencyInjection.ConfigSettings;
using MinioConsumer.Models;
using MinioConsumer.Services.PrimaryStorageSaver;
using MinioConsumer.Services;
using MinioConsumer.Services.Repositories;
using MinioConsumers.Services;
using MongoDB.Driver;
using StackExchange.Redis;

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
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<MetadataSaver<DocumentMetadata>>();
        services.AddScoped<PrimaryStorageSaver<DocumentMetadata>>();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
        });
    }

    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddControllers();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    internal static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.Configure<JwtSettings>(
            configuration.GetSection(JwtSettings.Jwt));

        serviceCollection
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException())),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false
                };
            });

        return serviceCollection;
    }
}
