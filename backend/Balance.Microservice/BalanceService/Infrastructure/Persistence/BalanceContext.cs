﻿using BalanceService.Domain;
using BalanceService.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BalanceService.Infrastructure.Persistence;

public class BalanceContext : DbContext
{
    public DbSet<Balance> Balances { get; set; }

    public DbSet<User> Users { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }

    public BalanceContext(DbContextOptions<BalanceContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
    }
}