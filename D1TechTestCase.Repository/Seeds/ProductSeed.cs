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
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasData(
                    new Product
                    {
                        Id = Guid.Parse("2757d9f5-e18c-4aa4-9bff-6809dde9d540"),
                        Name = "Kalem1",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Stock = 3,
                        Price = 12.00m,
                        CategoryId = Guid.Parse("83ff96df-8965-49f7-a13f-7aa4619aa207"),
                        
                    },
                    new Product
                    {
                        Id = Guid.Parse("32f1ffc7-4360-47d4-a6c7-b9ef19c1300f"),
                        Name = "Kalem2",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Stock = 2,
                        Price = 10.50m,
                        CategoryId = Guid.Parse("83ff96df-8965-49f7-a13f-7aa4619aa207"),
                        
                    },
                    new Product
                    {
                        Id = Guid.Parse("75703845-6ff0-46c2-97eb-372bda9b9bd5"),
                        Name = "Kitap1",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Stock = 5,
                        Price = 120.00m,
                        CategoryId = Guid.Parse("6050aff3-21f9-49d4-ad28-7bc40c980263"),
                        
                    },
                    new Product
                    {
                        Id = Guid.Parse("2040ebdf-7f17-4aa4-bbef-0e6faf5e8e1e"),
                        Name = "Kitap2",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Stock = 8,
                        Price = 99.99m,
                        CategoryId = Guid.Parse("6050aff3-21f9-49d4-ad28-7bc40c980263"),
                        
                    }
                );
        }
    }
}
