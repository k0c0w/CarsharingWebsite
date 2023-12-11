using Domain.Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.Chat;
using Persistence.Chat.ChatEntites.SignalRModels;
using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Persistence.RepositoryImplementation;

public class OccasionMessageRepository
{
    private readonly ChatContext _ctx;

    public OccasionMessageRepository(ChatContext context)
    {
        _ctx = context;
    }

    public async Task<Guid> AddAsync(OccasionMessage entity)
    {
        _ctx.Add(entity);
        await _ctx.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<IEnumerable<OccasionMessage>> GetBatchAsync(int? offset = default, int? limit = default)
    {
        IQueryable<OccasionMessage> messages = _ctx.OccasionMessages;

        if (offset != null)
            messages = messages.Skip(offset.Value);
        if (limit != null)
            messages = messages.Skip(limit.Value);

        return await messages.ToArrayAsync().ConfigureAwait(false);
    }

    public Task<OccasionMessage?> GetByIdAsync(Guid primaryKey)
    {
        return _ctx.OccasionMessages.FirstOrDefaultAsync(x => x.Id == primaryKey);
    }

    public async Task<IEnumerable<OccasionChatMessage>> GetMessagesAsync(Guid occasionId, int offset, int limit)
    {
        var result = await _ctx.OccasionMessages.ToListAsync();
        var history = await _ctx.OccasionMessages
                                .AsNoTracking()
                                .Where(m => m.OccasionId == occasionId)
                                .Join(
                                    _ctx.Users,
                                    m => m.AuthorId,
                                    u => u.Id,
                                    (m, u) =>
                                    new
                                    {
                                        m.Id,
                                        m.Text,
                                        m.Time,
                                        m.IsFromManager,
                                        u.FirstName,
                                        UserId = u.Id,
                                    }
                                )
                                .Skip(offset)
                                .Take(limit)
                                .OrderByDescending(x => x.Time)
                                .ToArrayAsync()
                                .ConfigureAwait(false);

        return history
            .Select(x => new OccasionChatMessage()
            {
                AuthorName = x.FirstName!,
                IsFromManager = x.IsFromManager,
                MessageId = x.Id,
                Text = x.Text,
                Time = x.Time,
            })
          .ToArray();
    }

}