using Features.Users.Shared;

namespace Carsharing.Helpers.Extensions.ServiceRegistration;

public static class BuisnessLogicServicesRegistrationExtension
{

    public static IServiceCollection RegisterBuisnessLogicServices(this IServiceCollection services)
    {
        services.AddRepositories();
        services.AddUnitsOfWork();

        services.AddScoped<UserValidation>();

        return services;
    }
}
