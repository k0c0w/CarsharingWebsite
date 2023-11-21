namespace Carsharing.Helpers.Extensions.ServiceRegistration;

public static class SwaggerRegistration
{
    public static IServiceCollection RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();

        return services;
    }
}
