using D1TechTestCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Repositories
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<Product> GetByIdWithFeature(Guid id);
        Task<Product> GetByIdWithFeatureTracking(Guid id);
        Task<List<Product>> GetAllWithFeature();
        Task<List<Product>> GetFavorites(Guid userId);
        Task AddFavorites(List<ProductUser> productUsers);
        void RemoveFavorites(List<ProductUser> productUsers);
    }
}
