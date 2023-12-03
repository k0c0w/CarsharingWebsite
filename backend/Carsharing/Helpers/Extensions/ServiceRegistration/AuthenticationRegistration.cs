using System.Text;
using Carsharing.Helpers.Authorization;
using Carsharing.Helpers.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Carsharing.Helpers.Extensions.ServiceRegistration;

public static class AuthenticationAndAuthorizationRegistration
{
    internal static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.Jwt));

        serviceCollection.AddTransient<IJwtGenerator, JwtGenerator>();

        serviceCollection
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException())),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

        return serviceCollection;
    }
}