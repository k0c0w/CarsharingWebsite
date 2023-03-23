using System;
using System.Collections.Generic;
using Carsharing.Model;
using Microsoft.EntityFrameworkCore;

namespace Carsharing;

public partial class CarsharingContext : DbContext
{
    public CarsharingContext()
    {
    }

    public CarsharingContext(DbContextOptions<CarsharingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CarModel> CarModels { get; set; }

    public virtual DbSet<CarPark> CarParks { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientInfo> ClientInfos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Tarrif> Tarrifs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Carsharing;Username=postgres;Password=g190703v");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("car_model_pkey");

            entity.ToTable("car_model");

            entity.HasIndex(e => e.Name, "car_model_name_key").IsUnique();

            entity.HasIndex(e => e.SourceImg, "car_model_source_img_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.SourceImg).HasColumnName("source_img");
            entity.Property(e => e.TarrifId).HasColumnName("tarrif_id");

            entity.HasOne(d => d.Tarrif).WithMany(p => p.CarModels)
                .HasForeignKey(d => d.TarrifId)
                .HasConstraintName("car_model_tarrif_id_fkey");
        });

        modelBuilder.Entity<CarPark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("car_park_pkey");

            entity.ToTable("car_park");

            entity.HasIndex(e => e.GovermentNumber, "car_park_goverment_number_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CarModelId).HasColumnName("car_model_id");
            entity.Property(e => e.GovermentNumber).HasColumnName("goverment_number");
            entity.Property(e => e.IsOpened)
                .HasDefaultValueSql("false")
                .HasColumnName("is_opened");
            entity.Property(e => e.IsTaken)
                .HasDefaultValueSql("false")
                .HasColumnName("is_taken");

            entity.HasOne(d => d.CarModel).WithMany(p => p.CarParks)
                .HasForeignKey(d => d.CarModelId)
                .HasConstraintName("car_park_car_model_id_fkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client");

            entity.HasIndex(e => e.RoleId, "Role_id_unique").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithOne(p => p.Client)
                .HasForeignKey<Client>(d => d.RoleId)
                .HasConstraintName("client_role_id_fkey");
        });

        modelBuilder.Entity<ClientInfo>(entity =>
        {
            entity.HasKey(e => new { e.PassportType, e.PassportNum, e.RiderNum, e.TelephoneNum }).HasName("client_info_pkey");

            entity.ToTable("client_info");

            entity.HasIndex(e => e.CardNumber, "client_info_card_number_key").IsUnique();

            entity.HasIndex(e => e.ClientId, "client_info_client_id_key").IsUnique();

            entity.Property(e => e.PassportType).HasColumnName("passport_type");
            entity.Property(e => e.PassportNum).HasColumnName("passport_num");
            entity.Property(e => e.RiderNum).HasColumnName("rider_num");
            entity.Property(e => e.TelephoneNum).HasColumnName("telephone_num");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Balance)
                .HasDefaultValueSql("0")
                .HasColumnName("balance");
            entity.Property(e => e.CardNumber).HasColumnName("card_number");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Surname).HasColumnName("surname");

            entity.HasOne(d => d.Client).WithOne(p => p.ClientInfo)
                .HasForeignKey<ClientInfo>(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_info_client_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("Name");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subscription_pkey");

            entity.ToTable("subscription");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CarId).HasColumnName("car_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("false")
                .HasColumnName("is_active");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Car).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("subscription_car_id_fkey");

            entity.HasOne(d => d.Client).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("subscription_client_id_fkey");
        });

        modelBuilder.Entity<Tarrif>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tarrif_pkey");

            entity.ToTable("tarrif");

            entity.HasIndex(e => e.Name, "tarrif_name_key").IsUnique();

            entity.HasIndex(e => e.Period, "tarrif_period_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Period).HasColumnName("period");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
