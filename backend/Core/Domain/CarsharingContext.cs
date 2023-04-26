using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain;

public class CarsharingContext : DbContext
{
    public CarsharingContext(DbContextOptions<CarsharingContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public virtual DbSet<CarModel> CarModels { get; set; }

    public virtual DbSet<Car> Cars { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserRole> Roles { get; set; }

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

        modelBuilder.Entity<UserRole>().Property(x => x.Id)
            .HasConversion(x => (int)x, x => (Roles)x);
        modelBuilder.Entity<User>().Property(x => x.RoleId)
            .HasConversion(x => (int)x, x => (Roles)x);
        
        
        modelBuilder.Entity<UserRole>()
            .HasData(new UserRole() { Id=Domain.Entities.Roles.Admin, Name = "admin" },
                     new UserRole() { Id=Domain.Entities.Roles.User, Name = "user" });
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
