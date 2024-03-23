using Domain.Repository;
using Entities.Repository;
using Persistence.Chat;
using Persistence.RepositoryImplementation;
using Persistence.UnitOfWork;

namespace Carsharing.Helpers.Extensions.ServiceRegistration;

public static class DataAccessRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<OccasionMessageRepository>();
        services.AddScoped<IPostRepository, NotUnitOfWorkPostRepository>();
        services.AddScoped<ISubscriptionRepository, >
        services.AddScoped<ITariffRepository, TariffRepository>();
        services.AddScoped<IOccasionRepository, OccasionRepository>();
        return services;
    }

    public static IServiceCollection AddUnitsOfWork(this IServiceCollection services)
    {
        services.AddScoped<IMessageUnitOfWork, ChatUnitOfWork>();
        services.AddScoped<IUnitOfWork, CarsharingUnitOfWork>();
        services.AddScoped<IUnitOfWork<ITariffRepository>, TariffUnitOfWork>();


        return services;
    }
}
