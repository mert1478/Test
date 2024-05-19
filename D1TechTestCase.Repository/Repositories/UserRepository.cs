using D1TechTestCase.Core.Entities;
using D1TechTestCase.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D1TechTestCase.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<User> GetUserByIdWithRoles(Guid userId)
        {
            var user = await _context.Users.AsNoTracking()
                .Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<User> GetUserByUsername(string userName)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.NormalizedUserName == userName);
            return user;
        }

        public async Task<List<UserSession>> GetUserSessionById(Guid userId)
        {
            var result = await _context.UserSessions.AsNoTracking().Where(us => us.UserId == userId).ToListAsync();
            return result;
        }
    }
}
