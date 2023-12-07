using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Migrations.Chat;
using Migrations.CarsharingApp;
using MassTransit;
using Carsharing.Helpers.Options;
using Features.PipelineBehavior;
using Features.Utils;
using FluentValidation;
using MediatR;

namespace Carsharing;

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
                /*cfg.Host(configuration
                        .GetSection(RabbitMqConfig.SectionName)
                        .Get<RabbitMqConfig>()!
                        .FullHostname);
                cfg.ConfigureEndpoints(ctx);
                */
            });
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

        services
         .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
         .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
         {
             options.Events.OnRedirectToLogin = context =>
             {
                 context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                 return Task.CompletedTask;
             };
             options.LoginPath = "/Login";

             options.Events.OnRedirectToAccessDenied = context =>
             {
                 context.Response.StatusCode = StatusCodes.Status403Forbidden;
                 return Task.CompletedTask;
             };
         });

        return services;
    }

    public static IServiceCollection AddMediatorWithFeatures(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

        return services;
    }
}