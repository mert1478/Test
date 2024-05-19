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
    public class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(
                    new ProductFeature
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.Parse("2757d9f5-e18c-4aa4-9bff-6809dde9d540"),
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Color = "Red",
                        Height = 13,
                        Width = 1
                    },
                    new ProductFeature
                    {
                        ProductId = Guid.Parse("32f1ffc7-4360-47d4-a6c7-b9ef19c1300f"),
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Color = "Blue",
                        Height = 14,
                        Width = 1
                    },
                    new ProductFeature
                    {
                        ProductId = Guid.Parse("75703845-6ff0-46c2-97eb-372bda9b9bd5"),
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Color = "white",
                        Height = 20,
                        Width = 3
                    },
                    new ProductFeature
                    {
                        ProductId = Guid.Parse("2040ebdf-7f17-4aa4-bbef-0e6faf5e8e1e"),
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        Color = "Red",
                        Height = 22,
                        Width = 4
                    }
                );
        }
    }
}
