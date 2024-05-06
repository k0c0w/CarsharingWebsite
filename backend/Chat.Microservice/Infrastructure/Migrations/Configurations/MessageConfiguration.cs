
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Migrations.Configurations;

internal class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.AuthorId);
    }
}
