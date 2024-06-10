using BaseProjectMvc_Net8.Data.Entities;

namespace BaseProjectMvc_Net8.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUserByEmail(string email);
    }
}
