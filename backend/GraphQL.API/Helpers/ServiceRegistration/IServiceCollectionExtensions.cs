using Carsharing.Consumers;
using Carsharing.Helpers.Options;
using Domain.Entities;
using Features.CarBooking.Commands.BookCar;
using Features.PipelineBehavior;
using Features.Tariffs.Admin;
using Features.Tariffs.Admin.Commands.CreateTariff;
using Features.Utils;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Migrations.Chat;
using Shared.Results;

namespace GraphQL.API.Helpers.ServiceRegistration;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationAssemblyName = typeof(CarsharingContext).Assembly.ToString();

        services.AddDbContext<CarsharingContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(migrationAssemblyName));
        });

        services.AddDbContext<ChatContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    public static IServiceCollection AddMassTransitWithRabbitMQProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(configuration
                        .GetSection(RabbitMqConfig.SectionName)
                        .Get<RabbitMqConfig>()!
                        .FullHostname);
                cfg.ConfigureEndpoints(ctx);
            });

            config.AddConsumer<OccasionStatusChangeConsumer>();
        });

        return services;
    }

    public static IServiceCollection AddIdentityAuthorization(this IServiceCollection services)
    {
        services.AddIdentity<User, UserRole>(options =>
        {
            options.User.AllowedUserNameCharacters = "user0123456789";
        })
        .AddEntityFrameworkStores<CarsharingContext>()
        .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddMediatorWithFeatures(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        {
            services.AddScoped(typeof(IPipelineBehavior<BookCarCommand, Result>), typeof(BookCarValidationBehavior));
            services.AddScoped(typeof(IPipelineBehavior<CreateTariffCommand, Result>), typeof(CreateTariffValidationBehavior));
        }
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

        return services;
    }
}