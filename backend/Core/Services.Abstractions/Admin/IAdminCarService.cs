using Contracts;

namespace Services.Abstractions.Admin;

public interface IAdminCarService : ICarService
{
    Task CreateModelAsync(CreateCarModelDto create);
    
    Task CreateCarAsync(CreateCarDto create);

    Task EditModelAsync(int id, EditCarModelDto update);
    
    Task TryDeleteModelAsync(int id);
    
    Task DeleteCarAsync(int id);
    
    Task<IEnumerable<CarModelDto>> GetAllModelsAsync();
    
    Task<IEnumerable<CarDto>> GetAllCarsAsync();
    
    Task<IEnumerable<CarDto>> GetCarsByModelAsync(int modelId);
    
    Task<IEnumerable<CarDto>> GetAvailableCarsByModelAsync(int modelId);
}