using BaseProjectMvc_Net8.Data.Context;
using BaseProjectMvc_Net8.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectMvc_Net8.Services
{
	public class UserService(ApplicationDbContext context) : IUserService
	{
		private readonly ApplicationDbContext _context = context;
		public const int TotalAllowedBeneficiaries = 5;

		public async Task<UserTest> AddUser(UserTest user)
		{
			user.IsUser = true;

			await _context.UsersTest.AddAsync(user);

			await _context.SaveChangesAsync();

			return user;
		}

		public async Task AddBeneficiaries(UserTest beneficiary)
		{
			UserTest user = new UserTest()!;

			var userExists = await _context.UsersTest.Include(x => x.Beneficiaries).FirstOrDefaultAsync(x => x.Id == beneficiary.UserId);

			if (userExists?.Beneficiaries?.Count < TotalAllowedBeneficiaries)
			{
				user.Id = beneficiary.Id;
				user.Name = beneficiary.Name;
				user.IsUser = false;
				user.UserId = beneficiary.UserId;

				await _context.UsersTest.AddAsync(user);

				await _context.SaveChangesAsync();
			}
		}

        public async Task CreateUser(User user)
        {
			await _context.Users.AddAsync(user);

			await _context.SaveChangesAsync();
        }
    }
}
