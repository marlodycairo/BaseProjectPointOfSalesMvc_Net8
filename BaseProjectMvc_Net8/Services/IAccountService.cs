using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Models;

namespace BaseProjectMvc_Net8.Services
{
    public interface IAccountService
    {
        Task<User> ValidateUser(LoginModel login); //debe retornar bool o info user
        Task RegisterUser(RegisterModel register);
    }
}
