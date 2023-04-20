using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder
            //.HasMany(entity => entity.Subscriptions)
            //.WithOne(x => x.User)
            ////.HasForeignKey(x => x.UserId)
            //.HasPrincipalKey(x => x.SubscriptionsId);


            builder
                .HasOne(entity => entity.UserInfo)
                .WithOne(entity => entity.User)
                .HasForeignKey<UserInfo>(entity => entity.UserId)
                .HasPrincipalKey<User>(entity => entity.Id);
        }
    }
}
