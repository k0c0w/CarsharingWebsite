using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Entities.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Migrations.CarsharingApp;

public class CarsharingContext : IdentityDbContext<User>
{
    public CarsharingContext(DbContextOptions<CarsharingContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public virtual DbSet<CarModel> CarModels { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public override DbSet<User> Users { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }
    
    public virtual DbSet<Tariff> Tariffs { get; set; }
    
    public virtual DbSet<Post> News { get; set; }
    
    public virtual DbSet<Document> WebsiteDocuments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SetDefaultValues(builder);
        SetUniqueFields(builder);

        builder.Entity<Tariff>()
            .ToTable(t =>
                t.HasCheckConstraint($"CK_{nameof(Tariff)}_{nameof(Tariff.Price)}",
                    $"\"{nameof(Tariff.Price)}\" > 0"));

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserInfoConfiguration());

        builder.Entity<CarModel>().Ignore(x => x.ImageName);

        var roles = new List<UserRole>()
        {
            new UserRole()
            {
                Id = "-1",
                Name = Role.Manager.ToString(),
                NormalizedName = Role.Manager.ToString().ToUpper(),
            },
            new UserRole()
            {
                Id = "-2",
                Name = Role.User.ToString(),
                NormalizedName = Role.User.ToString().ToUpper(),
            },
            new UserRole()
            {
                Id = "-3",
                Name = Role.Admin.ToString(),
                NormalizedName = Role.Admin.ToString().ToUpper(),
            }
        };

        builder.Entity<User>(b =>
        {
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        builder.Entity<UserRole>(b =>
        {
            // Each Role can have many entries in the UserRole join table
            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });

        builder.Entity<UserRole>().HasData(roles);
    }

    private static void SetUniqueFields(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity => { entity.HasIndex(e => e.Name).IsUnique(); });
        modelBuilder.Entity<Tariff>(entity => { entity.HasIndex(e => e.Name).IsUnique(); });
    }
    
    private static void SetDefaultValues(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().Property(x => x.IsOpened).HasDefaultValue(false);
        modelBuilder.Entity<Car>().Property(x => x.IsTaken).HasDefaultValue(false);
        modelBuilder.Entity<Car>().Property(x => x.HasToBeNonActive).HasDefaultValue(false);
        modelBuilder.Entity<Subscription>().Property(x => x.IsActive).HasDefaultValue(false);
        modelBuilder.Entity<Tariff>().Property(x => x.IsActive).HasDefaultValue(false);
    }
}
