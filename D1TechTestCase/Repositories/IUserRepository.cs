using D1TechTestCase.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Core.Repositories
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> GetUserById(Guid userId);
        Task<User> GetUserByUsername(string userName);
        Task<User> GetUserByIdWithRoles(Guid userId);
        Task<List<UserSession>> GetUserSessionById(Guid userId);
    }
}
