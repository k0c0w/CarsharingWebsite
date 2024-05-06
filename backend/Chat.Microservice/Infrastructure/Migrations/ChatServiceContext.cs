using Domain;
using Microsoft.EntityFrameworkCore;
using Migrations.Configurations;

namespace Migrations;

public class ChatServiceContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    public DbSet<User> User { get; set; }

    public ChatServiceContext(DbContextOptions<ChatServiceContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
