using BaseProjectMvc_Net8.Data.Context;
using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Models.Response;
using BaseProjectMvc_Net8.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectMvc_Net8.Repositories
{
    public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddCategory(Category category)
        {
            var parameters = new List<SqlParameter>
            {
                new() { ParameterName = "@Category_Name", Value = category.Category_Name}
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_AddCategory @Category_Name", parameters.ToArray());
        }

        public async Task DeleteCategory(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new() { ParameterName = "@Id", Value = id }
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteCategory @Id", parameters);
        }

        public async Task<GenericResponse<IEnumerable<Category>>> GetCategories()
        {
            var response = new GenericResponse<IEnumerable<Category>>
            {
                //response.Result = await _context.Categories.FromSqlRaw("EXEC sp_GetAllCategories").ToListAsync();

                Result = await _context.Categories.ToListAsync()
            };

            return response;
        }

        public async Task<GenericResponse<Category>> GetCategoryById(int id)
        {
            var response = new GenericResponse<Category>();

            var parameters = new List<SqlParameter>
            {
                new() { ParameterName = "@IdCategory", Value = id}
            };

            response.Result = _context.Categories.FromSqlRaw("EXEC sp_GetCategoryById @IdCategory", parameters.ToArray()).AsEnumerable().FirstOrDefault();

            return await Task.FromResult(response);
        }

        public async Task UpdateCategory(Category category)
        {
            var parameters = new List<SqlParameter>
            {
                new() { ParameterName = "@Id", Value = category.Id },
                new() { ParameterName = "@Category_name", Value = category.Category_Name }
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateCategory @Id, @Category_name", parameters);
        }
    }
}
