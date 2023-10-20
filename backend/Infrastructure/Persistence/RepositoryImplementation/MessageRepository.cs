using Domain.Entities;
using Entities.Exceptions;
using Microsoft.EntityFrameworkCore;
using Migrations.Chat;
using Persistence.Chat;
using Persistence.Chat.ChatEntites.SignalRModels;

namespace Persistence.RepositoryImplementation;

public class MessageRepository : IMessageRepository
{
    private readonly ChatContext _ctx;

    public MessageRepository(ChatContext context)
    {
        _ctx = context;
    }

    public Task<Guid> AddAsync(Message entity)
    {
        _ctx.Add(entity);

        return Task.FromResult(entity.Id);
    }

    public async Task<IEnumerable<Message>> GetBatchAsync(int? offset = default, int? limit = default)
    {
        IQueryable<Message> messages = _ctx.Messages;

        if (offset != null)
            messages = messages.Skip(offset.Value);
        if (limit != null)
            messages = messages.Skip(limit.Value);

        return await messages.ToArrayAsync().ConfigureAwait(false);
    }

    public Task<Message?> GetByIdAsync(Guid primaryKey)
    {
        return _ctx.Messages.FirstOrDefaultAsync(x => x.Id == primaryKey);
    }

    public async Task<IEnumerable<ChatMessage>> GetMessagesAssosiatedWithUserAsync(string userId, int offset, int limit)
    {
        var history = await _ctx.Messages
                                .AsNoTracking()
                                .Where(m => m.TopicAuthorId == userId)
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
            .Select(x => new ChatMessage()
            {
                AuthorName = x.FirstName!,
                IsFromManager = x.IsFromManager,
                MessageId = x.UserId,
                Text = x.Text,
                Time = x.Time,
            })
          .ToArray();
    }

    public async Task<Message> RemoveByIdAsync(Guid primaryKey)
    {
        var modelToRemove = await GetByIdAsync(primaryKey).ConfigureAwait(false);

        if (modelToRemove == null)
            throw new NotFoundException($"Id: {primaryKey}", typeof(Message));

        _ctx.Messages.Remove(modelToRemove);

        return modelToRemove;
    }

    public Task UpdateAsync(Message entity)
    {
        throw new NotImplementedException();
    }
}