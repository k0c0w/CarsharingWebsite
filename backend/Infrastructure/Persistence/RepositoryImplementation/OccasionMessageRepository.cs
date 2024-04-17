using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Migrations.Chat;
using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Persistence.RepositoryImplementation;

public class OccasionMessageRepository
{
    private readonly ChatContext _ctx;
    private readonly ILogger<OccasionMessageRepository> _logger;

    public OccasionMessageRepository(ChatContext context, ILogger<OccasionMessageRepository> logger)
    {
        _ctx = context;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(OccasionMessage entity)
    {
        _ctx.Add(entity);
        await _ctx.SaveChangesAsync();

        _logger.LogInformation("Saved message {message}", entity.Text);

        return entity.Id;
    }

    public async Task<IEnumerable<OccasionMessage>> GetBatchAsync(int? offset = default, int? limit = default)
    {
        IQueryable<OccasionMessage> messages = _ctx.OccasionMessages;

        if (offset != null)
            messages = messages.Skip(offset.Value);
        if (limit != null)
            messages = messages.Skip(limit.Value);

        return await messages.ToArrayAsync();
    }

    public Task<OccasionMessage?> GetByIdAsync(Guid primaryKey)
    {
        return _ctx.OccasionMessages.FirstOrDefaultAsync(x => x.Id == primaryKey);
    }

    public async Task<IEnumerable<OccasionChatMessage>> GetMessagesAsync(Guid occasionId, int offset, int limit)
    {
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
                                        m.Attachment
                                    }
                                )
                                .Skip(offset)
                                .Take(limit)
                                .OrderBy(x => x.Time)
                                .ToArrayAsync();

        return history
            .Select(x => new OccasionChatMessage()
            {
                AttachmentId = x.Attachment,
                AuthorName = x.FirstName!,
                IsFromManager = x.IsFromManager,
                MessageId = x.Id,
                Text = x.Text,
                Time = x.Time,
            })
          .ToArray();
    }
}