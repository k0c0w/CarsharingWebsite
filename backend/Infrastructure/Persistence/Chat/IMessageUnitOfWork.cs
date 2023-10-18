using Domain.Repository;
namespace Persistence.Chat;

public interface IMessageUnitOfWork : IUnitOfWork
{
    IMessageRepository MessageRepository { get; }

    void SaveChanges();
}
