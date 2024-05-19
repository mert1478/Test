using D1TechTestCase.Core.DTOs;
using D1TechTestCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Services
{
    public interface ICategoryService: IService<Category>
    {
        Task<List<CategoryWithProductDto>> GetCategoryWithProducts();
        Task<Category> GetCategorByIdWithProducts(Guid id);
    }
}
