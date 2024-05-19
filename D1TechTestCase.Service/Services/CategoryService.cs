using AutoMapper;
using D1TechTestCase.Core.DTOs;
using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Repositories;
using D1TechTestCase.Core.Services;
using D1TechTestCase.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<Category> repository, ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetCategorByIdWithProducts(Guid id)
        {
            var result = await _categoryRepository.GetCategorByIdWithProducts(id);
            return result;
        }

        public async Task<List<CategoryWithProductDto>> GetCategoryWithProducts()
        {
            var result = await _categoryRepository.GetCategoryWithProducts();
            var resultDto = _mapper.Map<List<CategoryWithProductDto>>(result);
            return resultDto;
        }
    }
}
