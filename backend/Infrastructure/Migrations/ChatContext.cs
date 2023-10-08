using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
