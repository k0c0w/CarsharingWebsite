using Microsoft.Extensions.Logging;
using Migrations.Chat;
using Persistence.Chat;
using Persistence.RepositoryImplementation;


namespace Persistence.UnitOfWork;

public class ChatUnitOfWork : IMessageUnitOfWork
{
    private readonly ChatContext _ctx;
    private readonly ILogger<ChatUnitOfWork> _logger;

    public IMessageRepository MessageRepository { get; }

    public ChatUnitOfWork(ChatContext context, ILogger<ChatUnitOfWork> logger)
    {
        _logger = logger;
        _ctx = context;
        MessageRepository = new MessageRepository(context);
    }

    public void SaveChanges()
    {
        _ctx.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        _logger.LogInformation("Saved messages");

        return _ctx.SaveChangesAsync();
    }
}
