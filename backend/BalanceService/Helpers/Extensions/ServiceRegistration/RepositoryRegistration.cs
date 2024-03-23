using BalanceService.Domain.Abstractions.DataAccess;
using BalanceService.Infrastructure.Repositories;

namespace BalanceService.Helpers.Extensions.ServiceRegistration;

public static class RepositoryRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IBalanceRepository, BalanceRepository>()
            .AddTransient<ITransactionRepository, TransactionRepository>();

        return serviceCollection;
    }
}