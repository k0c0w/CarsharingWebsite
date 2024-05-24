using Domain.Entities;
using Domain.Repository;

namespace Entities.Repository;

public interface ICarRepository : IRepository<Car, int>
{
    public Task<Car?> GetByLicensePlateAsync(string lp);

    public Task<bool> ExistsByIdAsync(int carId);

    public Task<Tariff?> GetRelatedTariffAsync(int carId);

    public Task<IEnumerable<Car>> GetCarsByModelIdAsync(int modelId);

    public Task RemoveAsync(Car car);
}
