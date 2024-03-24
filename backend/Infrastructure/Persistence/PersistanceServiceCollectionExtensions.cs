using Domain.Repository;
using Entities.Repository;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Chat;
using Persistence.RepositoryImplementation;
using Persistence.UnitOfWork;

namespace Persistence;

public static class PersistanceServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<OccasionMessageRepository>();
        services.AddScoped<IPostRepository, NotUnitOfWorkPostRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITariffRepository, TariffRepository>();
        services.AddScoped<IOccasionRepository, OccasionRepository>();
        return services;
    }

    public static IServiceCollection AddUnitsOfWork(this IServiceCollection services)
    {
        services.AddScoped<IMessageUnitOfWork, ChatUnitOfWork>();
        services.AddScoped<IUnitOfWork, CarsharingUnitOfWork>();


        services.AddScoped<IUnitOfWork<ITariffRepository>, CarsharingUnitOfWork<ITariffRepository>>();
        services.AddScoped<IUnitOfWork<ISubscriptionRepository>, CarsharingUnitOfWork<ISubscriptionRepository>>();
        services.AddScoped<IUnitOfWork<ICarRepository>, CarsharingUnitOfWork<ICarRepository>>();

        return services;
    }
}
