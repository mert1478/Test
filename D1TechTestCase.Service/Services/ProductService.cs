using AutoMapper;
using D1TechTestCase.Core.DTOs;
using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Models;
using D1TechTestCase.Core.Repositories;
using D1TechTestCase.Core.Services;
using D1TechTestCase.Core.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductUser> _productUserRepository;
        public ProductService(IGenericRepository<ProductUser> productUserRepository, IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productUserRepository = productUserRepository;
        }

        public async Task<ResponseModel<string>> AddFavorites(HttpContext httpContext, List<Guid> ids)
        {
            List<ProductUser> pUser = new List<ProductUser>();
            Guid userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            foreach (var id in ids)
            {
                bool checkProduct = await _productRepository.AnyAsync(x => x.Id == id);
                bool checkProductUser = await _productUserRepository.AnyAsync(pu => pu.UserId == userId && pu.ProductId == id);
                if(!checkProduct)
                {
                    return ResponseModel<string>.Fail(404, $"Product{id} not found");
                }
                if(!checkProductUser)
                {
                    pUser.Add(new ProductUser { UserId = userId, ProductId = id });
                }
            }
            try
            {
                await _productRepository.AddFavorites(pUser);
                await _unitOfWork.CommitAsync();
                return ResponseModel<string>.Success(200);
            }catch (Exception ex)
            {
                return ResponseModel<string>.Fail(500, ex.Message);
            }
        }

        public async Task<List<Product>> GetAllWithFeature()
        {
            var result = await _productRepository.GetAllWithFeature();
            return result;
        }

        public async Task<Product> GetByIdWithFeature(Guid id)
        {
            var result = await _productRepository.GetByIdWithFeature(id);
            return result;
        }

        

        public async Task<ResponseModel<List<ProductDto>>> GetFavorites(HttpContext httpContext)
        {
            Guid id = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _productRepository.GetFavorites(id);
            var resultDto = _mapper.Map<List<ProductDto>>(result);
            if(resultDto.Any()) { 
                return ResponseModel<List<ProductDto>>.Success(200,resultDto);
            }
            return ResponseModel<List<ProductDto>>.Fail(404, "Not fount favorites");
        }

        public async Task<ResponseModel<string>> RemoveFavorites(HttpContext httpContext, List<Guid> ids)
        {
            Guid userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            List<ProductUser> pUser = new List<ProductUser>();
            foreach (var id in ids)
            {
                bool check = await _productUserRepository.AnyAsync(pu => pu.UserId == userId && pu.ProductId == id);
                if(check)
                {
                    pUser.Add(new ProductUser { UserId = userId,ProductId = id});
                }
            }
            try
            {
                _productRepository.RemoveFavorites(pUser);
                await _unitOfWork.CommitAsync();
                return ResponseModel<string>.Success(200);
            }catch (Exception ex)
            {
                return ResponseModel<string>.Fail(500, ex.Message);
            }
        }

        public async Task<ProductDto> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            Product existProduct = await _productRepository.GetByIdWithFeatureTracking(productUpdateDto.Id);
            existProduct.CategoryId = productUpdateDto.CategoryId;
            existProduct.Stock = productUpdateDto.Stock;
            existProduct.Price = productUpdateDto.Price;
            existProduct.Name = productUpdateDto.Name;
            existProduct.ProductFeature.Width = productUpdateDto.ProductFeature.Width;
            existProduct.ProductFeature.Height = productUpdateDto.ProductFeature.Height;
            await _unitOfWork.CommitAsync();
            var productDto = _mapper.Map<ProductDto>(existProduct);
            return productDto;
        }
    }
}
