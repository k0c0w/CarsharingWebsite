using Domain.Entities;
using Domain.Repository;

namespace Entities.Repository;

public interface IPostRepository: IRepository<Post, int>
{
    Task<IEnumerable<Post>> GetPaginatedNoTrackingAsync(int page, int limit, bool byDescending); 
}