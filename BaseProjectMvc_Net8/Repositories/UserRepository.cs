using BaseProjectMvc_Net8.Data.Context;
using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectMvc_Net8.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
        }

		public async Task<IEnumerable<User>> GetCustomers()
		{
			return await _context.Users.Include(x => x.Role).Where(x => x.Role_Id == 4).ToListAsync();
		}

		public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email!.ToLower() == email.ToLower());

            return user!;
        }

		public async Task<IEnumerable<User>> GetUsers()
		{
            return await _context.Users.Include(x => x.Role).ToListAsync();
		}
	}
}
