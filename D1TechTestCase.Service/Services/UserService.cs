using AutoMapper;
using D1TechTestCase.Core.DTOs;
using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Models;
using D1TechTestCase.Core.Repositories;
using D1TechTestCase.Core.Services;
using D1TechTestCase.Core.UnitOfWorks;
using D1TechTestCase.Service.Crypto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;
        public UserService(IGenericRepository<User> repository, IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _UserRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<string>> ChangePassword(HttpContext httpContext, ChangePasswordModel changePasswordModel)
        {
            Guid userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var existUser = await _UserRepository.GetByIdAsync(userId);
            if(existUser == null)
            {
                return ResponseModel<string>.Fail(404, "User not found");
            }
            if(existUser.PasswordHash != CryptoService.ComputeSha256Hash(changePasswordModel.OldPassword))
            {
                return ResponseModel<string>.Fail(400, "Password wrong");
            }
            existUser.PasswordHash = CryptoService.ComputeSha256Hash(changePasswordModel.NewPassword);
            existUser.SecurityStamp = CryptoService.RandomStringGenerate(32);
            await _unitOfWork.CommitAsync();
            return ResponseModel<string>.Success(200);
        }

        public async Task<ResponseModel<string>> CreateUser(RegisterDto registerDto)
        {
            User user = _mapper.Map<User>(registerDto);
            bool check = await _UserRepository.AnyAsync(u => u.NormalizedUserName == user.NormalizedUserName);
            if(check)
            {
                return ResponseModel<string>.Fail(400, "Username already used");
            }
            try
            {
                await _UserRepository.AddAsync(user);
                await _unitOfWork.CommitAsync();
                return ResponseModel<string>.Success(200);
            }
            catch (Exception ex)
            {
                return ResponseModel<string>.Fail(500, ex.Message);
            }
            
            
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var result = await _UserRepository.GetUserById(userId);
            return result;
        }

        public async Task<User> GetUserByIdWithRoles(Guid userId)
        {
            var result = await _UserRepository.GetUserByIdWithRoles(userId);
            return result;
        }

        public async Task<User> GetUserByUsername(string userName)
        {
            var result = await _UserRepository.GetUserByUsername(userName);
            return result;
        }

        public async Task<ResponseModel<List<UserSessionDto>>> GetSession(HttpContext httpContext)
        {
            Guid userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _UserRepository.GetUserSessionById(userId);
            if (!result.Any())
            {
                return ResponseModel<List<UserSessionDto>>.Fail(404, "Not found");
            }
            var resultDto = _mapper.Map<List<UserSessionDto>>(result);
            
            return ResponseModel<List<UserSessionDto>>.Success(200, resultDto);
        }

        public async Task<ResponseModel<UserDto>> UpdateProfile(HttpContext httpContext, UpdateProfileModel updateProfileModel)
        {
            Guid userId = Guid.Parse(httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var existUser = await _UserRepository.GetByIdAsync(userId);
            if(existUser == null)
            {
                return ResponseModel<UserDto>.Fail(404, "User not found");
            }
            existUser.Name = updateProfileModel.Name;
            existUser.Surname = updateProfileModel.Surname;
            await _unitOfWork.CommitAsync();
            return ResponseModel<UserDto>.Success(200,_mapper.Map<UserDto>(existUser));
        }
    }
}
