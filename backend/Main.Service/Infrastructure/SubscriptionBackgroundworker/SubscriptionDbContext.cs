using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SubscriptionBackgroundworker;

public class SubscriptionDbContext : DbContext
{
    public DbSet<Subscription> Subscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Carsharing;Username=postgres;Password=112112aA");
    }
}