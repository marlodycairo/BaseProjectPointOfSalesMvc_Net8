using BaseProjectMvc_Net8.Data.Context;
using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Models;
using BaseProjectMvc_Net8.Repositories.Interfaces;

namespace BaseProjectMvc_Net8.Services
{
	public class AccountService(ApplicationDbContext context, IUserRepository userRepository) : IAccountService
	{
		private readonly ApplicationDbContext _context = context;
		private readonly IUserRepository _userRepository = userRepository;

		public async Task<User> ValidateUser(LoginModel login)
		{
			var user = await _userRepository.GetUserByEmail(login.Email!);

			if (user.Email!.Equals(login.Email) && user.Password!.Equals(login.Pass))
			{
				return user;
			}

			return null!;
		}

		public async Task RegisterUser(RegisterModel register)
		{
			var user = new User
			{
				FirstName = register.FirstName,
				LastName = register.LastName,
				Address = register.Address,
				Phone = register.Phone,
				Email = register.Email,
				Username = register.Username,
				Password = register.Password,
				Role_Id = register.Role_Id
			};

			await _context.Users.AddAsync(user);

			await _context.SaveChangesAsync();
		}
	}
}
