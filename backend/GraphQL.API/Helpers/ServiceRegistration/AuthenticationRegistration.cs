using System.Text;
using Carsharing.Helpers.Authorization;
using Carsharing.Helpers.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GraphQL.API.Helpers.ServiceRegistration;

public static class AuthenticationAndAuthorizationRegistration
{
    internal static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.Jwt))
            .AddAuthorization();

        serviceCollection.AddTransient<IJwtGenerator, JwtGenerator>();

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
                    ValidateIssuerSigningKey = true
                };
            });

        serviceCollection
            .AddHttpContextAccessor()
            .AddTransient<HttpTrackerHandler>()
            .AddHttpClient("authorized")
            .AddHttpMessageHandler<HttpTrackerHandler>();

        return serviceCollection;
    }
}