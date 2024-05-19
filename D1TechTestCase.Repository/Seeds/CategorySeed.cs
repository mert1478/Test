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
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasData(
                    new Category
                    {
                        Id = Guid.Parse("83ff96df-8965-49f7-a13f-7aa4619aa207"),
                        Name = "Kalemler",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    },
                    new Category
                    {
                        Id = Guid.Parse("6050aff3-21f9-49d4-ad28-7bc40c980263"),
                        Name = "Kitaplar",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    }
                );
        }
    }
}
