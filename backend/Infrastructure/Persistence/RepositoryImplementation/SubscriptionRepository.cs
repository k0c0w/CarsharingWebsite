using Domain.Entities;
using Domain.Repository;
using Entities.Repository;
using Migrations.CarsharingApp;
using Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.RepositoryImplementation;

public class SubscriptionRepository : ISubscriptionRepository
{


    public Task<int> AddAsync(Subscription entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Subscription>> GetActiveSubscriptionsByCarIdAsync(int carId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Subscription>> GetBatchAsync(int? offset = null, int? limit = null)
    {
        throw new NotImplementedException();
    }

    public Task<Subscription?> GetByIdAsync(int primaryKey)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Subscription>> GetSubscriptionsByCarIdAsync(int carId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveByIdAsync(int primaryKey)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Subscription entity)
    {
        throw new NotImplementedException();
    }
}