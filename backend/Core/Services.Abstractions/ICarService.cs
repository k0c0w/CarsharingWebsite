using Contracts;

namespace Services.Abstractions;

public interface ICarService
{
    Task ReleaseCarAsync(int carId);

    Task<bool> SetCarIsTakenAsync(int id);

    Task<IEnumerable<CarModelDto>> GetModelsByTariffIdAsync(int tariff);
    
    Task<ExtendedCarModelDto> GetModelByIdAsync(int id);

    Task<IEnumerable<FreeCarDto>> GetAvailableCarsByLocationAsync(SearchCarDto searchParams, int limit=256);
    Task<string> OpenCar(int carId);
    Task<string> CloseCar(int carId);
}