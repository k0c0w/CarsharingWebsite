using Domain.Entities;
using Domain.Repository;

namespace Entities.Repository;

public interface ISubscriptionRepository : IRepository<Subscription, int>
{
    //todo: change to spec
    public Task<IEnumerable<Subscription>> GetActiveSubscriptionsByCarIdAsync(int carId);


    public Task<IEnumerable<Subscription>> GetSubscriptionsByCarIdAsync(int carId);

    public Task RemoveAsync(Subscription subscription);
}
