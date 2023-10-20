using Persistence.Chat;

namespace Carsharing.Helpers.Extensions.ServiceRegistration;

public static class DataAccessRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, IMessageRepository>();
        return services;
    }

    public static IServiceCollection AddUnitsOfWork(this IServiceCollection services)
    {
        services.AddScoped<IMessageUnitOfWork, IMessageUnitOfWork>();

        return services;
    }
}
