using Domain;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Migrations.Configurations;

namespace Migrations;

public class ChatServiceContext : DbContext
{
    public DbSet<Message> Messages { get; set; }

    public DbSet<User> Users { get; set; }

    public ChatServiceContext(DbContextOptions<ChatServiceContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();

        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
