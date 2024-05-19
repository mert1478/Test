using AutoMapper;
using D1TechTestCase.Core.DTOs;
using D1TechTestCase.Core.Entities;
using D1TechTestCase.Service.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Service.Mapping
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<CategorySaveDto, Category>();
            CreateMap<ProductSaveDto, Product>();
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
            CreateMap<UserSession, SessionDto>();
            CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => CryptoService.ComputeSha256Hash(src.Password)))
            .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => CryptoService.RandomStringGenerate(32)))
            //.ForMember(dest => dest.Roles, opt => opt.MapFrom(src => new List<Role> { new Role { Name = "DefaultRole" } }))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()));
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductDto>();

            CreateMap<CategorySaveDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<ProductUpdateDto, Product>();

            CreateMap<Category, CategoryWithProductDto>();

            CreateMap<User, UserDto>();
            CreateMap<UserSession, UserSessionDto>();
        }
    }
}
