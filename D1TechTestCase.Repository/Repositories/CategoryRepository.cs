using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetCategorByIdWithProducts(Guid id)
        {
            var result = await _context.Categories
                .Include(c => c.Products)
                .ThenInclude(p => p.ProductFeature)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task<List<Category>> GetCategoryWithProducts()
        {
            var result = await _context.Categories
                .Include(c => c.Products)
                .ThenInclude(p => p.ProductFeature)
                .AsNoTracking().ToListAsync();
            return result;
        }
    }
}
