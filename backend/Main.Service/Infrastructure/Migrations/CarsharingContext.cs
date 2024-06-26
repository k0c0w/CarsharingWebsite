using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Entities.EntityConfigurations;
using Entities.Entities;
using Migrations.EntityConfigurations;
using MassTransit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Migrations.CarsharingApp;

public class CarsharingContext : IdentityDbContext<User>
{
    public CarsharingContext(DbContextOptions<CarsharingContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public virtual DbSet<OccasionMessage> OccasionMessages { get; set; }

    public virtual DbSet<CarModel> CarModels { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public override DbSet<User> Users { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Tariff> Tariffs { get; set; }

    public virtual DbSet<Post> News { get; set; }

    public virtual DbSet<Document> WebsiteDocuments { get; set; }

    public virtual DbSet<OccasionType> OccasionTypes { get; set; }
    public virtual DbSet<Occassion> Occasions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        AddOutboxEntites(builder);

        SetDefaultValues(builder);
        SetUniqueFields(builder);

        builder.Entity<Tariff>()
            .ToTable(t =>
                t.HasCheckConstraint($"CK_{nameof(Tariff)}_{nameof(Tariff.PricePerMinute)}",
                    $"\"{nameof(Tariff.PricePerMinute)}\" > 0"));

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserInfoConfiguration());

        var roles = new List<UserRole>()
        {
            new ()
            {
                Id = "-1",
                Name = Role.Manager.ToString(),
                NormalizedName = Role.Manager.ToString().ToUpper(),
            },
            new ()
            {
                Id = "-2",
                Name = Role.User.ToString(),
                NormalizedName = Role.User.ToString().ToUpper(),
            },
            new ()
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

        builder.ApplyConfiguration(new OccasionTypeConfiguration());
        builder.ApplyConfiguration(new OccasionConfiguration());
    }

    private static void SetUniqueFields(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity => { entity.HasIndex(e => e.Name).IsUnique(); entity.HasKey(e => e.FileName); });
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

    private static void AddOutboxEntites(ModelBuilder builder)
    {
        builder.AddInboxStateEntity();
        builder.AddOutboxStateEntity();
        builder.AddOutboxMessageEntity();
    }
}
