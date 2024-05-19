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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddFavorites(List<ProductUser> productUsers)
        {
            await _context.ProductsUsers.AddRangeAsync(productUsers);
        }

        public async Task<List<Product>> GetAllWithFeature()
        {
            var result = await _context.Products
                .Include(p => p.ProductFeature)
                .AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Product> GetByIdWithFeature(Guid id)
        {
            var result = await _context.Products
                .Include(p => p.ProductFeature)
                .AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return result;
        }

        public async Task<Product> GetByIdWithFeatureTracking(Guid id)
        {
            var result = await _context.Products
                .Include(p => p.ProductFeature)
                .FirstOrDefaultAsync(p => p.Id == id);
            return result;
        }

        public async Task<List<Product>> GetFavorites(Guid userId)
        {
            var result = await _context.Users.SelectMany(u => u.Favorites).Include(p => p.ProductFeature).AsNoTracking().ToListAsync();
            return result;
        }

        public void RemoveFavorites(List<ProductUser> productUsers)
        {
             _context.ProductsUsers.RemoveRange(productUsers);
        }
    }
}
