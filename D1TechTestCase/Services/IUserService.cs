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
    public interface IUserService : IService<User>
    {
        Task<ResponseModel<string>> CreateUser(RegisterDto registerDto);
        Task<User> GetUserById(Guid userId);
        Task<User> GetUserByUsername(string userName);
        Task<User> GetUserByIdWithRoles(Guid userId);
        Task<ResponseModel<UserDto>> UpdateProfile(HttpContext httpContext, UpdateProfileModel updateProfileModel);
        Task<ResponseModel<string>> ChangePassword(HttpContext httpContext, ChangePasswordModel changePasswordModel);
        Task<ResponseModel<List<UserSessionDto>>> GetSession(HttpContext httpContext);
    }
}
