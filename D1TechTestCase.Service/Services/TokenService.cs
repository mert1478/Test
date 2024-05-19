using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Models;
using D1TechTestCase.Core.Services;
using D1TechTestCase.Service.Crypto;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserService _userService;
        private readonly TokenOption _tokenOption;
        public TokenService(IUserService userService, IOptions<TokenOption> options)
        {
            _userService = userService;
            _tokenOption = options.Value;
        }
        private string CreateRefreshToken()
        {
            string random = CryptoService.RandomStringGenerate(32);

            return random;
        }
        private static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
        private IEnumerable<Claim> GetClaims(User user, List<String> audiences, string sessionId)
        {

            var userList = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            //new Claim(ClaimTypes.Name,user.Name),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim("SecurityStamp",user.SecurityStamp),
            new Claim("SessionId",sessionId)
            };
            foreach (var role in user.Roles)
            {
                userList.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userList;
        }
        public TokenModel CreateToken(User user, string sessionId)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            
            var securityKey = GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: GetClaims(user, _tokenOption.Audience, sessionId),
                 signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenModel = new TokenModel
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration,
                AccessTokenType = _tokenOption.TokenType,
            };

            return tokenModel;
        }
    }
}
