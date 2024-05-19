using AutoMapper;
using D1TechTestCase.API.Filters;
using D1TechTestCase.Core.DTOs;
using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Models;
using D1TechTestCase.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace D1TechTestCase.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    [EnableRateLimiting("Basic")]
    public class AdminController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public AdminController(ICategoryService categoryService, IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _productService = productService;
        }
        [HttpGet("Category")]
        public async Task<IActionResult> GetCategory(bool includeProducts = false)
        {
            if(includeProducts)
            {
                var result = await _categoryService.GetCategoryWithProducts();               
                return CreateActionResult(ResponseModel<List<CategoryWithProductDto>>.Success(200, result));
            }
            var singleResult = await _categoryService.GetAllAsync();
            var singleResultDto = _mapper.Map<List<CategoryDto>>(singleResult);
            return CreateActionResult(ResponseModel<List<CategoryDto>>.Success(200, singleResultDto));
        }
        [HttpGet("Category/{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        public async Task<IActionResult> GetSingleCategory(Guid id)
        {
            var result = await _categoryService.GetCategorByIdWithProducts(id);
            var resultDto = _mapper.Map<CategoryWithProductDto>(result);
            return CreateActionResult(ResponseModel<CategoryWithProductDto>.Success(200, resultDto));
        }
        [HttpPost("Category")]
        public async Task<IActionResult> SaveCategory(CategorySaveDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);
            await _categoryService.AddAsync(category);
            return CreateActionResult(ResponseModel<Category>.Success(200, category));
        }
        [HttpPut("Category")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateDto categoryUpdateDto)
        {
            bool check = await _categoryService.AnyAsync(c => c.Id == categoryUpdateDto.Id);
            if (!check)
            {
                return CreateActionResult(ResponseModel<string>.Fail(404, "Not Found"));
            }
            Category category = _mapper.Map<Category>(categoryUpdateDto);
            await _categoryService.UpdateAsync(category);
            return CreateActionResult(ResponseModel<NoContentModel>.Success(200));
        }
        [HttpDelete("Category")]
        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var exist = await _categoryService.GetByIdAsync(id);
            await _categoryService.RemoveAsync(exist);
            return CreateActionResult(ResponseModel<NoContentModel>.Success(200));
        }
        [HttpGet("Product")]
        public async Task<IActionResult> GetProduct(bool includeFeature = false)
        {
            if (includeFeature)
            {
                var resultft = await _productService.GetAllWithFeature();
                var resultftDto = _mapper.Map<List<ProductDto>>(resultft);
                return CreateActionResult(ResponseModel<List<ProductDto>>.Success(200, resultftDto));
            }
            var result = await _productService.GetAllAsync();
            var resultDto = _mapper.Map<List<ProductDto>>(result);
            return CreateActionResult(ResponseModel<List<ProductDto>>.Success(200, resultDto));
        }
        [HttpGet("Product/{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> GetSingleProduct(Guid id)
        {
            var result = await _productService.GetByIdAsync(id);
            return CreateActionResult(ResponseModel<Product>.Success(200, result));
        }
        [HttpPost("Product")]
        public async Task<IActionResult> SaveProduct(ProductSaveDto productSaveDto)
        {
            bool check = await _categoryService.AnyAsync(c => c.Id == productSaveDto.CategoryId);
            if(!check)
            {
                return CreateActionResult(ResponseModel<NoContentModel>.Fail(404, $"Category{productSaveDto.CategoryId} not found"));
            }
            Product product = _mapper.Map<Product>(productSaveDto);
            await _productService.AddAsync(product);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(ResponseModel<ProductDto>.Success(200, productDto));
        }
        [HttpPut("Product")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            bool check = await _productService.AnyAsync(p => p.Id == productUpdateDto.Id);
            bool checkCategory = await _categoryService.AnyAsync(c => c.Id == productUpdateDto.CategoryId);
            if(!check)
            {
                return CreateActionResult(ResponseModel<NoContentModel>.Fail(404,$"Product{productUpdateDto.Id} not found"));
            }
            if (!checkCategory)
            {
                return CreateActionResult(ResponseModel<NoContentModel>.Fail(404, $"Category{productUpdateDto.CategoryId} not found"));
            }
             var result = await _productService.UpdateProduct(productUpdateDto);
             return CreateActionResult(ResponseModel<ProductDto>.Success(200, result));                        
        }
        [HttpDelete("Product")]
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var exist = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(exist);
            return CreateActionResult(ResponseModel<NoContentModel>.Success(200));
        }
    }
}
