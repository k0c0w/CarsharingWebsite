using Domain;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Migrations;

namespace Persistence.RepositoriesImplementations;

public class MessageRepository(ChatServiceContext context) : IMessageRepository
{
    private readonly ChatServiceContext _ctx = context;

    public async Task AddAsync(Message message)
    {
        //todo: maybe save in background?
        await _ctx.AddAsync(message);
        await _ctx.SaveChangesAsync();
    }

    public async Task<IEnumerable<MessageAggregate>> GetMessagesByTopicAsync(string topic, int limit = 100, int offset = 0)
    {
        return await _ctx.Messages
            .AsNoTracking()
            .Join(
                _ctx
                    .Users
                    .AsNoTracking(),
                msg => msg.AuthorId,
                usr => usr.Id,
                (msg, usr) => new MessageAggregate(msg, usr)
            )
            .Skip(offset)
            .Take(limit)
            .ToArrayAsync();
    }
}
