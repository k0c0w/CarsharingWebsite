namespace Services.Abstractions;

public interface ICarService
{
    Task ReleaseCarAsync(int carId);

    Task<bool> SetCarIsTakenAsync(int id);
    
    
}