using BalanceService.Domain;
using BalanceService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BalanceService.Infrastructure.Persistence.EntityConfigurations;


internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, val => new UserId(val));
        
        builder.Property(x => x.BalanceId)
            .HasConversion(id => id.Value, val => new BalanceId(val));

        builder.HasOne(user => user.Balance)
            .WithOne(balance => balance.User)
            .HasForeignKey<Balance>(x => x.UserId)
            .IsRequired();
    }
}
