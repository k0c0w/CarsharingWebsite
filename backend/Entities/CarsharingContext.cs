using Entities.EntityConfigurations;
using Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Common;

namespace Entities;

public class CarsharingContext : IdentityDbContext<User>
{
    public CarsharingContext(DbContextOptions<CarsharingContext> options) : base(options)
    {
        //DbContext.Database.EnsureCreated();
    }
    
    public virtual DbSet<CarModel> CarModels { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public override DbSet<User> Users { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }
    
    public virtual DbSet<Tariff> Tariffs { get; set; }
    
    public virtual DbSet<Post> News { get; set; }
    
    public virtual DbSet<Document> WebsiteDocuments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        SetDefaultValues(modelBuilder);
        SetUniqueFields(modelBuilder);

        modelBuilder.Entity<Tariff>()
            .ToTable(t =>
                t.HasCheckConstraint($"CK_{nameof(Tariff)}_{nameof(Tariff.Price)}",
                    $"\"{nameof(Tariff.Price)}\" > 0"));

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserInfoConfiguration()); 
    }

    private void SetUniqueFields(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity => { entity.HasIndex(e => e.Name).IsUnique(); });
        modelBuilder.Entity<Tariff>(entity => { entity.HasIndex(e => e.Name).IsUnique(); });
    }
    
    private void SetDefaultValues(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().Property(x => x.IsOpened).HasDefaultValue(false);
        modelBuilder.Entity<Car>().Property(x => x.IsTaken).HasDefaultValue(false);
        modelBuilder.Entity<Car>().Property(x => x.HasToBeNonActive).HasDefaultValue(false);
        modelBuilder.Entity<Subscription>().Property(x => x.IsActive).HasDefaultValue(false);
        modelBuilder.Entity<Tariff>().Property(x => x.IsActive).HasDefaultValue(false);
    }
}
