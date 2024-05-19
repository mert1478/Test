using D1TechTestCase.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Repository.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(e => e.NormalizedUserName);
            builder
                .HasMany(e => e.Favorites)
                .WithMany(e => e.Users)
                .UsingEntity<ProductUser>(
                    p => p
                        .HasOne(e => e.Product)
                        .WithMany()
                        .HasForeignKey(e => e.ProductId)
                        .OnDelete(DeleteBehavior.Cascade),
                    p => p
                        .HasOne(e => e.User)
                        .WithMany()
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.Cascade)
                );
            builder
                .HasMany(e => e.Roles)
                .WithMany(e => e.Users)
                .UsingEntity<RoleUser>(
                    r => r
                    .HasOne(e => e.Role)
                    .WithMany()
                    .HasForeignKey(r => r.RoleId)
                    .OnDelete(DeleteBehavior.Cascade),
                    p => p
                    .HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    
                );
        }
    }
}
