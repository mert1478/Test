using D1TechTestCase.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Repository.Seeds
{
    public class RoleUserSeed : IEntityTypeConfiguration<RoleUser>
    {
        public void Configure(EntityTypeBuilder<RoleUser> builder)
        {
            builder.HasData(
                    new RoleUser
                    {
                       RoleId = Guid.Parse("7d5af191-85f8-4281-96ad-270348b77d88"),
                       UserId = Guid.Parse("64e12255-c39c-4129-805a-d1d62043f356")
                    }
                );
        }
    }
}
