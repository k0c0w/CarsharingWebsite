using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Repositories
{
    public interface IRepository<TId, TModel>
    {
        Task<TModel> GetByIdAsync(TId id);

        Task AddAsync(TModel model);

        Task RemoveByIdAsync(TId id);

        Task UpdateAsync(TModel model);
    }
}
