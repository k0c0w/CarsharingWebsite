using Domain;
using Domain.Entities;
using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Migrations.Chat;

public class ChatContext : IdentityDbContext<User>
{
    public DbSet<Message> Messages { get; set; }
    
    public DbSet<OccasionMessage> OccasionMessages { get; set; }

    public override DbSet<User> Users { get; set; }

    public ChatContext(DbContextOptions<ChatContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}
