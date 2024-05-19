using D1TechTestCase.Core.DTOs;
using D1TechTestCase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseModel<TokenModel>> CreateTokenAsync(LoginModel loginModel, string oS, string browser);

        Task<ResponseModel<TokenModel>> CreateTokenByRefreshToken(string refreshToken, string oS, string browser);

        Task<ResponseModel<NoContentModel>> RevokeAllRefreshToken(string refreshToken);

        Task<ResponseModel<NoContentModel>> RevokeRefreshToken(string refreshToken);

        Task<ResponseModel<NoContentModel>> RevokeAnotherRefreshToken(LogOutAnotherModel logOutOfSingleModel);
        Task<ResponseModel<List<SessionDto>>> GetSessions(Guid UserId, Guid sessionId);
    }
}
