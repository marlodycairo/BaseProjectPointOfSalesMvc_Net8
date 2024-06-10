using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Models.Response;

namespace BaseProjectMvc_Net8.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<GenericResponse<IEnumerable<Category>>> GetCategories();
        Task<GenericResponse<Category>> GetCategoryById(int id);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}
