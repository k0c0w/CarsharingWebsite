using Domain.Repository;
using Entities.Repository;
using Microsoft.Extensions.DependencyInjection;
using Persistence.RepositoryImplementation;
using Persistence.UnitOfWork;

namespace Persistence;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<OccasionMessageRepository>();
        services.AddScoped<IPostRepository, NotUnitOfWorkPostRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITariffRepository, TariffRepository>();
        services.AddScoped<IOccasionRepository, OccasionRepository>();
        services.AddScoped<ICarRepository, CarRepository>();
        return services;
    }

    public static IServiceCollection AddUnitsOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, CarsharingUnitOfWork>();


        services.AddScoped<IUnitOfWork<ITariffRepository>, CarsharingUnitOfWork<ITariffRepository>>();
        services.AddScoped<IUnitOfWork<ISubscriptionRepository>, CarsharingUnitOfWork<ISubscriptionRepository>>();
        services.AddScoped<IUnitOfWork<ICarRepository>, CarsharingUnitOfWork<ICarRepository>>();
        services.AddScoped<IUnitOfWork<IUserRepository>, CarsharingUnitOfWork<IUserRepository>>();

        return services;
    }
}
