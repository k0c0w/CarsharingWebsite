using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Chat.ChatEntites.DomainModels;

namespace Migrations.Chat
{
    public class ChatContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public ChatContext(DbContextOptions<CarsharingContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
