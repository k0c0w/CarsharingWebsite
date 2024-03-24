using BalanceService.Domain;
using BalanceService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalanceService.Infrastructure.Persistence.EntityConfigurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, val => new TransactionId(val));
        //
        // builder.Property(x => x.BalanceId)
        //     .HasConversion(id => id.Value, val => new BalanceId(val));
    }
}