using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Services
{
    public interface ITokenService
    {
        TokenModel CreateToken(User user, string sessionId);
    }
}
