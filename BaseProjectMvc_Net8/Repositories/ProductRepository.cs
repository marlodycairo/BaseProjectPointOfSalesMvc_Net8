using BaseProjectMvc_Net8.Data.Context;
using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Models;
using BaseProjectMvc_Net8.Models.Response;
using BaseProjectMvc_Net8.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
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
            //var parameters = new List<SqlParameter>
            //{
            //    new() { ParameterName = "@Product_Name", Value = product.Product_Name},
            //    new() { ParameterName = "@Price", Value = product.Price},
            //    new() { ParameterName = "@Category_Id", Value = product.Category_Id}
            //};

            //await _context.Database.ExecuteSqlRawAsync("EXEC sp_AddProduct @Product_Name, @Price, @Category_Id", parameters.ToArray());
        }

        public async Task DeleteProduct(int id)
        {
            var product = await GetProductById(id);

            _context.Products.Remove(product.Result!);

            await _context.SaveChangesAsync();
            //var parameters = new List<SqlParameter>
            //{
            //    new() { ParameterName = "@Id", Value = id }
            //};

            //await _context.Database.ExecuteSqlRawAsync("EXEC DeleteProduct @Id", parameters);
        }

        public async Task<GenericResponse<Product>> GetProductById(int id)
        {
            var response = new GenericResponse<Product>();

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            response.Result = product;

            return response;
            //var parameters = new List<SqlParameter>
            //{
            //    new() { ParameterName = "@Id", Value = id}
            //};

            //response.Result = _context.Products.FromSqlRaw("EXEC GetProductById @Id", parameters.ToArray()).AsEnumerable().FirstOrDefault();

            //return await Task.FromResult(response);
        }

        public async Task<GenericResponse<IEnumerable<Product>>> GetProducts()
        {
            var response = new GenericResponse<IEnumerable<Product>>();

            response.Result = await _context.Products.ToListAsync();

            return response;
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync();
            //var parameters = new List<SqlParameter>
            //{
            //    new() { ParameterName = "@Id", Value = product.Id},
            //    new() { ParameterName = "@Product_Name", Value = product.Product_Name},
            //    new() { ParameterName = "@Price", Value = product.Price},
            //    new() { ParameterName = "@Category_Id", Value = product.Category_Id}
            //};

            //await _context.Database.ExecuteSqlRawAsync("EXEC UpdateProduct @Id, @Product_Name, @Price, @Category_Id", parameters);
        }

        public async Task<GenericResponse<List<ItemModel>>> GetProductByName(string productName)
        {
            var result = await _context.Products
                .Where(x => x.Product_Name.ToLower().Contains(productName.ToLower()))
                .Select(x => new ItemModel
                {
                    Product_Name = x.Product_Name,
                    Product_Id = x.Id,
                    Price = x.Price
                }).ToListAsync();

            //var result = await (from p in _context.Products
            //              join i in _context.Inventories
            //              on p.Id equals i.Product_Id
            //              let productNameToLower = p.Product_Name.ToLower()
            //              where productNameToLower.Contains(productName.ToLower())
            //                  select new ItemModel
            //                  {
            //                      Product_Id = p.Id,
            //                      Product_Name = p.Product_Name,
            //                      Price = p.Price,
            //                      Quantity = i.Stock
            //                  }).ToListAsync();

            var products = new GenericResponse<List<ItemModel>>
            {
                Result = result
            };

            return products;
        }
    }
}
