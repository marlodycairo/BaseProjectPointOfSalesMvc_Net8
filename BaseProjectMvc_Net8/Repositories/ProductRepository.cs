using BaseProjectMvc_Net8.Data.Context;
using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Models;
using BaseProjectMvc_Net8.Models.Response;
using BaseProjectMvc_Net8.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectMvc_Net8.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : IProductRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await GetProductById(id);

            _context.Products.Remove(product.Result!);

            await _context.SaveChangesAsync();
        }

        public async Task<GenericResponse<Product>> GetProductById(int id)
        {
            var response = new GenericResponse<Product>();

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            response.Result = product;

            return response;
        }

        public async Task<GenericResponse<IEnumerable<Product>>> GetProducts()
        {
            var response = new GenericResponse<IEnumerable<Product>>
            {
                Result = await _context.Products.ToListAsync()
            };

            return response;
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync();
        }

        public async Task<GenericResponse<List<ItemModel>>> GetProductByName(string productName)
        {
            var products = new GenericResponse<List<ItemModel>>
            {
                Result = await _context.Products
                   .Where(x => x.Product_Name.ToLower() == productName.ToLower())
                   .Select(x => new ItemModel
                   {
                       Product_Name = x.Product_Name,
                       Product_Id = x.Id,
                       Price = x.Price
                   }).ToListAsync()
            };

            return products;
        }
    }
}
