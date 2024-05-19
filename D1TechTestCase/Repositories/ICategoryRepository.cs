using D1TechTestCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<List<Category>> GetCategoryWithProducts();
        Task<Category> GetCategorByIdWithProducts(Guid id);
    }
}
