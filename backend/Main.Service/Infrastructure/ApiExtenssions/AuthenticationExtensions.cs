using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Migrations.CarsharingApp;
using ApiExtensions.Options;
using ApiExtensions.Authorization;
using CommonExtensions.Jwt;
using CommonExtensions.Authorization;


namespace ApiExtensions;

public static class AuthenticationAndAuthorizationRegistration
{
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

    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection serviceCollection, IConfiguration configuration, Func<JwtBearerEvents>? configureEvents = null)
    {
        serviceCollection.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.Jwt));

        serviceCollection.AddTransient<IJwtGenerator, JwtGenerator>();

        serviceCollection
            .AddConfiguredAuthentication()
            .AddJwtBearer(configuration, configureEvents);
        serviceCollection.
            AddAuthorization();

        serviceCollection
            .AddHttpContextAccessor()
            .AddTransient<HttpTrackerHandler>()
            .AddHttpClient("authorized")
            .AddHttpMessageHandler<HttpTrackerHandler>();

        return serviceCollection;
    }
}
