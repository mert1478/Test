using D1TechTestCase.Core.DTOs;
using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Services
{
    public interface IProductService: IService<Product>
    {
        Task<Product> GetByIdWithFeature(Guid id);
        Task<List<Product>> GetAllWithFeature();
        Task<ResponseModel<List<ProductDto>>> GetFavorites(HttpContext httpContext);
        Task<ResponseModel<string>> AddFavorites(HttpContext httpContext, List<Guid> ids);
        Task<ResponseModel<string>> RemoveFavorites(HttpContext httpContext, List<Guid> ids);
        Task<ProductDto> UpdateProduct(ProductUpdateDto productUpdateDto);
    }
}
