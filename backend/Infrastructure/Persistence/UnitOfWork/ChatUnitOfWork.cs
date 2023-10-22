using Migrations.Chat;
using Persistence.Chat;
using Persistence.RepositoryImplementation;


namespace Persistence.UnitOfWork;

public class ChatUnitOfWork : IMessageUnitOfWork
{
    private readonly ChatContext _ctx;

    public IMessageRepository MessageRepository { get; }

    public ChatUnitOfWork(ChatContext context)
    {
        _ctx = context;
        MessageRepository = new MessageRepository(context);
    }

    public void SaveChanges()
    {
        _ctx.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return _ctx.SaveChangesAsync();
    }
}
