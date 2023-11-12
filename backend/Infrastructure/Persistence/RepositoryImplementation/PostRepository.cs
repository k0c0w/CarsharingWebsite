using Domain.Entities;
using Entities.Exceptions;
using Entities.Repository;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Persistence.RepositoryImplementation;

public class NotUnitOfWorkPostRepository : IPostRepository
{
    private readonly CarsharingContext _context;

    public NotUnitOfWorkPostRepository(CarsharingContext carsharingContext) 
    {
        _context = carsharingContext;
    }

    public async Task<int> AddAsync(Post entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<Post>> GetBatchAsync(int? offset = null, int? limit = null)
    {
        IQueryable<Post> news = _context.News;

        if (offset.HasValue)
            news = news.Skip(offset.Value);

        if (limit.HasValue)
            news = news.Take(limit.Value);

        return await news.ToArrayAsync();
    }

    public Task<Post?> GetByIdAsync(int primaryKey)
    {
        return _context.News.FirstOrDefaultAsync(x => x.Id == primaryKey);
    }

    public async Task<IEnumerable<Post>> GetPaginatedNoTrackingAsync(int page, int limit, bool byDescending)
    {
        IQueryable<Post> query = byDescending ?
        _context.News.OrderByDescending(x => x.CreatedAt) : _context.News;

       return await query
            .Skip((page - 1) * limit)
            .Take(limit)
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async Task<Post> RemoveByIdAsync(int primaryKey)
    {
        var postToRemove = await GetByIdAsync(primaryKey);

        if (postToRemove == null)
            throw new NotFoundException($"Not found by id on deletion: {primaryKey}", typeof(Post));

        _context.News.Remove(postToRemove);
        await _context.SaveChangesAsync();

        return postToRemove;
    }

    public async Task UpdateAsync(Post entity)
    {
        _context.News.Update(entity);
        await _context.SaveChangesAsync();
    }
}
