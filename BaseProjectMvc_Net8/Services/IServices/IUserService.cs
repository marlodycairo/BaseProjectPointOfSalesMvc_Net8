using BaseProjectMvc_Net8.Data.Entities;

namespace BaseProjectMvc_Net8.Services.IServices
{
    public interface IUserService
    {
        Task<UserTest> AddUser(UserTest user);
        Task AddBeneficiaries(UserTest user);

        Task CreateUser(User user);
        //Task<IEnumerable<User>> GetUsers();
        //Task<User> GetUserById(int id);
        //Task UpdateUser(User user);
        //Task DeleteUser(int id);
    }
}