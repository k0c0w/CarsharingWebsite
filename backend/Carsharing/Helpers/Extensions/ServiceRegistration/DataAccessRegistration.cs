using Persistence.Chat;
using Persistence.RepositoryImplementation;
using Persistence.UnitOfWork;

namespace Carsharing.Helpers.Extensions.ServiceRegistration;

public static class DataAccessRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
        return services;
    }

    public static IServiceCollection AddUnitsOfWork(this IServiceCollection services)
    {
        services.AddScoped<IMessageUnitOfWork, ChatUnitOfWork>();

        return services;
    }
}
