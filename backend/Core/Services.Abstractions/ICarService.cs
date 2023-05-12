using Contracts;

namespace Services.Abstractions;

public interface ICarService
{
    Task ReleaseCarAsync(int carId);

    Task<bool> SetCarIsTakenAsync(int id);

    Task<IEnumerable<CarModelDto>> GetModelsByTariffIdAsync(int tariff);
    
    Task<CarModelDto> GetModelByTariffIdAsync(int tariff);

    Task<IEnumerable<CarDto>> GetAvailableCarsByModelAsync(int modelId);
}