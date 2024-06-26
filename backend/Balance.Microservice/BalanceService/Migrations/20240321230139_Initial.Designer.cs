﻿// <auto-generated />
using System;
using BalanceService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BalanceService.Migrations
{
    [DbContext(typeof(BalanceContext))]
    [Migration("20240321230139_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BalanceService.Domain.Balance", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Savings")
                        .HasColumnType("numeric");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("BalanceService.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BalanceService.Domain.Balance", b =>
                {
                    b.HasOne("BalanceService.Domain.User", null)
                        .WithOne("Balance")
                        .HasForeignKey("BalanceService.Domain.Balance", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BalanceService.Domain.User", b =>
                {
                    b.Navigation("Balance")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
