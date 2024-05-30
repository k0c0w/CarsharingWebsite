using CommonExtensions.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Chat.Helpers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthorization(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection
            .AddConfiguredAuthentication()
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

        serviceCollection.AddAuthorization();

        return serviceCollection;
    }
}
