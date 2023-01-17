using Microsoft.EntityFrameworkCore;
using WasteRecyclingManagementApi.Core.Entities;
using WasteRecyclingManagementApi.Core.Repositories;

namespace WasteRecyclingManagementApi.Data.Repositories
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        public UserRepository(RecyclingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetUserWithCredentialsAsync(string username, string hash)
        {
            return await _dbContext.Users
                .Include(u => u.Operations)
                .FirstOrDefaultAsync(u => u.Username == username && 
                                          u.Password == hash);
        }

        public async Task<User> RegisterUserAsync(string username, string hash)
        {
            var user = new User { Username = username, Password = hash, Role = 0 };
            await AddAsync(user);
            return user;
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return await _dbContext.Users
                .Include(u => u.Operations)
                .ThenInclude(o => o.Container)
                .ThenInclude(c => c.RecyclingPoint)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserAsync(string username)
        {
            return await _dbContext.Users
                .Include(u => u.Operations)
                .ThenInclude(o => o.Container)
                .ThenInclude(c => c.RecyclingPoint)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

    }
}
