using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(entity => entity.UserInfo)
                .WithOne(entity => entity.User)
                .HasForeignKey<UserInfo>(entity => entity.UserId)
                .HasPrincipalKey<User>(entity => entity.Id);
        }
    }
}
