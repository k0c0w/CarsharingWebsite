using BalanceService.Domain;
using BalanceService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalanceService.Infrastructure.Persistence.EntityConfigurations;

public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
{
    public void Configure(EntityTypeBuilder<Balance> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, val => new BalanceId(val));
        
        builder.Property(x => x.UserId)
            .HasConversion(id => id.Value, val => new UserId(val));
    }
}
