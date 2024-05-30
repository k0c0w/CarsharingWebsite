using Domain.Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Migrations.EntityConfigurations;

internal class OccasionConfiguration : IEntityTypeConfiguration<Occassion>
{
    public void Configure(EntityTypeBuilder<Occassion> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder.Property(x => x.OccasionType)
            .HasConversion(to => (int)to, from => (OccasionTypeDefinition)from);

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.IssuerId);
    }
}

internal class OccasionTypeConfiguration : IEntityTypeConfiguration<OccasionType>
{
    public void Configure(EntityTypeBuilder<OccasionType> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(
            new OccasionType() { Id = (int)OccasionTypeDefinition.RoadAccident, Description = "ДТП" },
            new OccasionType() { Id = (int)OccasionTypeDefinition.VehicleBreakdown, Description = "Поломка ТС" },
            new OccasionType() { Id = (int)OccasionTypeDefinition.Other, Description = "Прочее" });
    }
}
