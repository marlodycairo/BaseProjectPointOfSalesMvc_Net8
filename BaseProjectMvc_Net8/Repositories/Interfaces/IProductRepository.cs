using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Models;
using BaseProjectMvc_Net8.Models.Response;

namespace BaseProjectMvc_Net8.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<GenericResponse<IEnumerable<Product>>> GetProducts();
        Task<GenericResponse<Product>> GetProductById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<GenericResponse<List<ItemModel>>> GetProductByName(string productName);
    }
}
