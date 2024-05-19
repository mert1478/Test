using AutoMapper;
using D1TechTestCase.API.Filters;
using D1TechTestCase.Core.DTOs;
using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Models;
using D1TechTestCase.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Net.Http;
using System.Security.Claims;

namespace D1TechTestCase.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("Basic")]
    public class UserController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(ICategoryService categoryService, IProductService productService, IMapper mapper, IUserService userService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("category")]
        public async Task<IActionResult> GetAllCategory()
        {
            var resul = await _categoryService.GetAllAsync();
            var resultDto = _mapper.Map<List<CategoryDto>>(resul);
            return CreateActionResult(ResponseModel<List<CategoryDto>>.Success(200, resultDto));
        }
        [HttpGet("category/{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        public async Task<IActionResult> GetSingleCategory(Guid id)
        {
            var result = await _categoryService.GetCategorByIdWithProducts(id);
            var resultDto = _mapper.Map<CategoryWithProductDto>(result);
            return CreateActionResult(ResponseModel<CategoryWithProductDto>.Success(200, resultDto));
        }
        [HttpGet("product")]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productService.GetAllWithFeature();
            var resultDto = _mapper.Map<List<ProductDto>>(result);
            return CreateActionResult(ResponseModel<List<ProductDto>>.Success(200, resultDto));
        }
        [HttpGet("product/{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> GetSingleProduct(Guid id)
        {
            var result = await _productService.GetByIdWithFeature(id);
            var resultDto = _mapper.Map<ProductDto>(result);
            return CreateActionResult(ResponseModel<ProductDto>.Success(200, resultDto));
        }
        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavorites()
        {
            var result = await _productService.GetFavorites(HttpContext);
            return CreateActionResult(result);
        }
        [HttpPost("favorites")]
        public async Task<IActionResult> AddFavorites(FavoritesIdsModel favoritesIdsModel)
        {
            var result = await _productService.AddFavorites(HttpContext, favoritesIdsModel.Ids);
            return CreateActionResult(result);
        }
        [HttpDelete("favorites")]
        public async Task<IActionResult> DeleteFavorites(FavoritesIdsModel favoritesIdsModel)
        {
            var result = await _productService.RemoveFavorites(HttpContext, favoritesIdsModel.Ids);
            return CreateActionResult(result);
        }
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            Guid userId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _userService.GetByIdAsync(userId);
            var resultDto = _mapper.Map<UserDto>(result);
            return CreateActionResult(ResponseModel<UserDto>.Success(200, resultDto));
        }
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileModel updateProfileModel)
        {
            var result = await _userService.UpdateProfile(HttpContext, updateProfileModel);
            return CreateActionResult(result);
        }
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var result = await _userService.ChangePassword(HttpContext, changePasswordModel);
            return CreateActionResult(result);
        }
        [HttpGet("session")]
        public async Task<IActionResult> GetSession()
        {
            var result = await _userService.GetSession(HttpContext);
            return CreateActionResult(result);
        }
    }
}
