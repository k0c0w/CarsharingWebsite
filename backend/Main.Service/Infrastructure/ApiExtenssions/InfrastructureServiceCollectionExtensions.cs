using Domain.Entities;
using Features.CarBooking.Commands.BookCar;
using Features.PipelineBehavior;
using Features.Tariffs.Admin.Commands.CreateTariff;
using Features.Tariffs.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Migrations.CarsharingApp;
using static MassTransit.ValidationResultExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Features.Utils;
using FluentValidation;
using MediatR;
using System.Reflection;
using ApiExtensions.Options;

namespace ApiExtensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationAssemblyName = typeof(CarsharingContext).Assembly.ToString();

        services.AddDbContext<CarsharingContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                x =>
                {
                    x.MigrationsAssembly(migrationAssemblyName);
                    x.MinBatchSize(1);
                });
        });

        return services;
    }

    public static IServiceCollection AddMassTransitWithRabbitMQProvider(this IServiceCollection services, IConfiguration configuration, Assembly consumersAssembly)
    {
        services.AddMassTransit(config =>
        {
            config.AddEntityFrameworkOutbox<CarsharingContext>(cfg =>
            {
                cfg
                .UsePostgres()
                .UseBusOutbox();
            });

            var currentAssembly = consumersAssembly;
            config.AddConsumers(currentAssembly);
            config.AddActivities(currentAssembly);

            config.UsingRabbitMq((ctx, cfg) =>
            {
                var rc = configuration
                        .GetSection(nameof(RabbitMqConfig))
                        .Get<RabbitMqConfig>()!;
                cfg.Host(rc.Host, rbc =>
                {
                    rbc.Username(rc.Username);
                    rbc.Password(rc.Password);
                });
                cfg.ConfigureEndpoints(ctx);
            });

        });

        return services;
    }

    public static IServiceCollection AddMediatorWithFeatures(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(FeturesAssemblyReference.Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        {
            services.AddScoped(typeof(IPipelineBehavior<BookCarCommand, Results.Result>), typeof(BookCarValidationBehavior));
            services.AddScoped(typeof(IPipelineBehavior<CreateTariffCommand, Results.Result>), typeof(CreateTariffValidationBehavior));
        }
        services.AddValidatorsFromAssembly(FeturesAssemblyReference.Assembly);

        return services;
    }
}
