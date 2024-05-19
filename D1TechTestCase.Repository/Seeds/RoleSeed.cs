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
    public class RoleSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { Id = Guid.Parse("7d5af191-85f8-4281-96ad-270348b77d88"), CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, Name = "Admin"});
        }
    }
}
