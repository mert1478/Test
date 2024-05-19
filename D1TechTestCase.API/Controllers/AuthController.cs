using AutoMapper;
using D1TechTestCase.Core.DTOs;
using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Models;
using D1TechTestCase.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UAParser;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D1TechTestCase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Basic")]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;       
        private readonly IUserService _userService;
        public AuthController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            string userAgentString = Request.Headers["User-Agent"].ToString();
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgentString);
            var result = await _authenticationService.CreateTokenAsync(loginModel, c.OS.ToString(), c.UA.ToString());
            return CreateActionResult(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginByRefreshToken(RefreshTokenModel refreshTokenModel)
        {
            string userAgentString = Request.Headers["User-Agent"].ToString();
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgentString);
            var result = await _authenticationService.CreateTokenByRefreshToken(refreshTokenModel.Token, c.OS.ToString(), c.UA.ToString());

            return CreateActionResult(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LogOut(RefreshTokenModel refreshTokenModel)
        {
            var result = await _authenticationService.RevokeRefreshToken(refreshTokenModel.Token);

            return CreateActionResult(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _userService.CreateUser(registerDto);
            return CreateActionResult(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SecureLogOut(RefreshTokenModel refreshTokenModel)
        {
            var result = await _authenticationService.RevokeAllRefreshToken(refreshTokenModel.Token);
            return CreateActionResult(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LogOutAnother(LogOutAnotherModel logOutAnotherModel)
        {
            var result = await _authenticationService.RevokeAnotherRefreshToken(logOutAnotherModel);
            return CreateActionResult(result);
        }

    }
}
