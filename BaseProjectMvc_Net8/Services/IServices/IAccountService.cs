using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Models;

namespace BaseProjectMvc_Net8.Services.IServices
{
    public interface IAccountService
    {
        Task<User> ValidateUser(LoginModel login);
        Task RegisterUser(RegisterModel register);
    }
}
