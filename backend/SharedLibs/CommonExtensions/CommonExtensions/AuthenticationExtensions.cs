using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace CommonExtensions.Authorization;

public static class AuthenticationExtensions
{
    public static AuthenticationBuilder AddConfiguredAuthentication(this IServiceCollection servicesCollection)
    {
        return servicesCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
    }
}
