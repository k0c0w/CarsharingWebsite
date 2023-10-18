using Services.Abstractions.Admin;
using Services.Abstractions;
using Services;
using Carsharing.Services;
using Services.User;

namespace Carsharing.Helpers.Extensions.ServiceRegistration;

public static class BuisnessLogicServicesRegistrationExtension
{

    public static IServiceCollection RegisterBuisnessLogicServices(this IServiceCollection services)
    {
        services.AddRepositories();
        services.AddUnitsOfWork();

        services.AddScoped<ITariffService, TariffService>();
        services.AddScoped<IAdminTariffService, TariffService>();
        services.AddScoped<IAdminCarService, CarService>();
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IFileProvider, FileProvider>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAdminPostService, PostService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IBalanceService, BalanceService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
