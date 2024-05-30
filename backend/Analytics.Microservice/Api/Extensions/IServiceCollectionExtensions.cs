using Analytics.Microservice.HostedServices;
using Analytics.Microservice.Options;
using Domain;
using LinqToDB;
using MassTransit;
using Microsoft.Extensions.Options;
using Persistence.DataAccess;
using Persistence.Services;
using Persistence.Transport.Consumers;
using RabbitMQ.Client;
using CommonExtensions.Jwt;
using CommonExtensions.Authorization;

namespace Analytics.Microservice.Extensions;

public static class IServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthenticationAndAuthorization(configuration);

        services.AddGrpc();

        services.AddClickHouseDb(configuration);

        services.AddTransient<IStatisticsRepository, StatisticsRepository>()
                .AddScoped<IStatisticsPublisher, StatisticsPublisher>();

        services.AddTransport(configuration);

        services.AddHostedServices();
    }

    private static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<ExchangeDeclarationHostedService>();
        services.AddHostedService<DatabaseMigrationHostedService>();
        services.AddHostedService<StatisticsNotifierBackgroundWorker>();

        return services;
    }

    private static IServiceCollection AddClickHouseDb(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new DataOptions()
            .UseClickHouse(LinqToDB.DataProvider.ClickHouse.ClickHouseProvider.ClickHouseClient)
            .UseConnectionString(configuration.GetConnectionString("ClickHouse") ?? throw new InvalidOperationException("Provide connection string to ClickHouse."))
            .UseMappingSchema(MappingSchema.TariffUsageScheme);

        services.AddTransient(sp =>
        {
            return new ClickHouseDb(options);
        });

        return services;
    }

    private static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection
            .AddConfiguredAuthentication()
            .AddJwtBearer(configuration);

        serviceCollection.AddAuthorization();

        return serviceCollection;
    }

    private static IServiceCollection AddTransport(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqConfig>(configuration.GetRequiredSection(nameof(RabbitMqConfig)));
        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<CarBookedEventConsumer>();

            configure.UsingRabbitMq((ctx, cfg) =>
            {
                var rc = configuration
                        .GetSection(nameof(RabbitMqConfig))
                        .Get<RabbitMqConfig>() ?? throw new InvalidOperationException($"Provide {nameof(RabbitMqConfig)}");

                cfg.Host(rc.Host, rbc =>
                {
                    rbc.Username(rc.Username);
                    rbc.Password(rc.Password);
                });

                cfg.ConfigureEndpoints(ctx);
            });

        });

        services.AddScoped<IModel>(sp =>
        {
            var rabbitConfig = sp.GetService<IOptions<RabbitMqConfig>>()!.Value;

            var factory = new ConnectionFactory()
            {
                Uri = new Uri(rabbitConfig.Host),
                CredentialsProvider = new BasicCredentialsProvider(rabbitConfig.Username, rabbitConfig.Password)
            };
            var connection = factory.CreateConnection();

            return connection.CreateModel();
        });

        return services;
    }
}
