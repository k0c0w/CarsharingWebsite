using Domain.Entities;
using Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Migrations.Chat;
using Microsoft.Extensions.Options;
using Migrations.CarsharingApp;

namespace Carsharing;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarsharingContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("Domain"));
        });

        services.AddDbContext<ChatContext>(options => {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("Migrations"));
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
}