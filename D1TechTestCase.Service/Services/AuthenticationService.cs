using AutoMapper;
using D1TechTestCase.Core.DTOs;
using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Models;
using D1TechTestCase.Core.Repositories;
using D1TechTestCase.Core.Services;
using D1TechTestCase.Core.UnitOfWorks;
using D1TechTestCase.Service.Crypto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserSession> _userSessionService;
        private readonly IMapper _mapper;
        public AuthenticationService(IMapper mapper, ITokenService tokenService, IUnitOfWork unitOfWork, IGenericRepository<UserSession> userSessionService, IUserService userService)
        {
            
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _userSessionService = userSessionService;
            _userService = userService;
            _mapper = mapper;
        }
        internal static string NormalizeUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return string.Empty;

            // Trim leading and trailing whitespaces
            username = username.Trim();

            // Convert to lowercase
            username = username.ToLowerInvariant();

            // Normalize the string to Form C (Canonical Decomposition followed by Canonical Composition)
            username = username.Normalize(NormalizationForm.FormC);

            // Replace spaces with underscores (optional, depending on your requirements)
            // username = username.Replace(' ', '_');

            // Remove diacritics (accents)
            var stringBuilder = new StringBuilder();
            foreach (var c in username.Normalize(NormalizationForm.FormD))
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        public async Task<ResponseModel<TokenModel>> CreateTokenAsync(LoginModel loginModel, string oS, string browser)
        {
            if (loginModel == null) throw new ArgumentNullException(nameof(loginModel));
            string normalizeUsername = NormalizeUsername(loginModel.Username);
            var user = await _userService.GetUserByUsername(normalizeUsername);


            if (user == null) return ResponseModel<TokenModel>.Fail(401, "Email or Password is wrong");

            

            if (user.PasswordHash != CryptoService.ComputeSha256Hash(loginModel.Password)) //!loginResult.IsSuccessStatusCode
            {
                return ResponseModel<TokenModel>.Fail(401, "Email or Password is wrong");
            }
            var sessionId = Guid.NewGuid();
            var token = _tokenService.CreateToken(user, sessionId.ToString());
            var rtoken = CryptoService.ComputeSha256Hash(token.RefreshToken);
            await _userSessionService.AddAsync(new UserSession {Id = sessionId ,UserId = user.Id, Code = rtoken, Expiration = token.RefreshTokenExpiration, Browser = browser, OS = oS });

            await _unitOfWork.CommitAsync();

            return ResponseModel<TokenModel>.Success(200, token);
        }

        public async Task<ResponseModel<TokenModel>> CreateTokenByRefreshToken(string refreshToken, string oS, string browser)
        {
            var rtoken = CryptoService.ComputeSha256Hash(refreshToken);
            var existRefreshToken = await _userSessionService.Where(x => x.Code == rtoken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return ResponseModel<TokenModel>.Fail(404, "Refresh token not found");
            }
            if (existRefreshToken.Expiration < DateTime.Now)
            {
                return ResponseModel<TokenModel>.Fail(401, "Refresh token has expired");
            }
            var user = await _userService.GetUserByIdWithRoles(existRefreshToken.UserId);  //null dönmeyebilir kontrol et.

            if (user == null)
            {
                return ResponseModel<TokenModel>.Fail(404, "User Id not found");
            }

            var tokenDto = _tokenService.CreateToken(user, existRefreshToken.Id.ToString());
            var rdto = CryptoService.ComputeSha256Hash(tokenDto.RefreshToken);
            existRefreshToken.Code = rdto;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return ResponseModel<TokenModel>.Success(200, tokenDto);
        }

        public async Task<ResponseModel<List<SessionDto>>> GetSessions(Guid UserId, Guid sessionId)
        {
            var sessions = await _userSessionService.Where(s => s.Id != sessionId && s.UserId == UserId).ToListAsync();
            var result = _mapper.Map<List<SessionDto>>(sessions);
            return ResponseModel<List<SessionDto>>.Success(200, result);
        }

  

        public async Task<ResponseModel<NoContentModel>> RevokeAllRefreshToken(string refreshToken)
        {
            var rtoken = CryptoService.ComputeSha256Hash(refreshToken);
            var existRefreshToken = await _userSessionService.Where(x => x.Code == rtoken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return ResponseModel<NoContentModel>.Fail(404, "Refresh token not found");
            }
            if (existRefreshToken.Expiration < DateTime.Now)
            {
                return ResponseModel<NoContentModel>.Fail(401, "Refresh token is expired");
            }
            var allTokens = _userSessionService.Where(r => r.UserId == existRefreshToken.UserId);
            _userSessionService.RemoveRange(allTokens);
            var user = await _userService.GetByIdAsync(existRefreshToken.UserId);
            if (user == null)
            {
                return ResponseModel<NoContentModel>.Fail(404, "User Not Found");
            }

            string securityStamp = CryptoService.RandomStringGenerate(32);

            user.SecurityStamp = securityStamp;
            await _unitOfWork.CommitAsync();
            return ResponseModel<NoContentModel>.Success(200);
        }

        public async Task<ResponseModel<NoContentModel>> RevokeAnotherRefreshToken(LogOutAnotherModel logOutOfSingleModel)
        {
            var rtoken = CryptoService.ComputeSha256Hash(logOutOfSingleModel.Token);
            var existRefreshToken = await _userSessionService.Where(x => x.Code == rtoken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return ResponseModel<NoContentModel>.Fail(404, "Refresh token not found");
            }
            if (existRefreshToken.Expiration < DateTime.Now)
            {
                return ResponseModel<NoContentModel>.Fail(401, "Refresh token is expired");
            }
            var tokenToRemove = await _userSessionService
                        .Where(t => t.Id == logOutOfSingleModel.SessionId && t.UserId == existRefreshToken.UserId).SingleOrDefaultAsync();
            if (tokenToRemove == null)
            {
                return ResponseModel<NoContentModel>.Fail(404, "No such session found");
            }
            _userSessionService.Remove(tokenToRemove);
            var user = await _userService.GetByIdAsync(existRefreshToken.UserId);
            if (user == null)
            {
                return ResponseModel<NoContentModel>.Fail(404, "User Not Found");
            }

            string securityStamp = CryptoService.RandomStringGenerate(32);
            user.SecurityStamp = securityStamp;
            await _unitOfWork.CommitAsync();

            return ResponseModel<NoContentModel>.Success(200);
        }

        public async Task<ResponseModel<NoContentModel>> RevokeRefreshToken(string refreshToken)
        {
            var rtoken = CryptoService.ComputeSha256Hash(refreshToken);
            var existRefreshToken = await _userSessionService.Where(x => x.Code == rtoken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return ResponseModel<NoContentModel>.Fail(404, "Refresh token not found");
            }

            _userSessionService.Remove(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return ResponseModel<NoContentModel>.Success(200);
        }
    }
}
