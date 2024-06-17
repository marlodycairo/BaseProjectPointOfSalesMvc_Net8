using BaseProjectMvc_Net8.Data.Context;
using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Models;
using BaseProjectMvc_Net8.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace BaseProjectMvc_Net8.Services
{
    public class AccountService(ApplicationDbContext context) : IAccountService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<User> ValidateUser(LoginModel login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(login.Email, StringComparison.CurrentCultureIgnoreCase));

           return  user.Email.Equals(login.Email, StringComparison.CurrentCultureIgnoreCase)
            && user.Password.Equals(login.Pass, StringComparison.CurrentCultureIgnoreCase)
            ? user : null;
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