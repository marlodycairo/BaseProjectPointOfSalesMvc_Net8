using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectMvc_Net8.Controllers
{
    public class ProductController(IProductRepository productRepository) : Controller
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<IActionResult> Index()
        {
            var result = await _productRepository.GetProducts();

            return View(result.Result);
        }

        [HttpGet]
        public  IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _productRepository.AddProduct(product);

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _productRepository.GetProductById(id);

            return View(result.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _productRepository.GetProductById(id);

            return View(result.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            await _productRepository.UpdateProduct(product);

            var productUpdated = new Product();

            var response = await _productRepository.GetProductById(product.Id);

            productUpdated = response.Result;

            return View("Details", productUpdated);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = new Product();

            var response = await _productRepository.GetProductById(id);

            product = response.Result;

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteProduct(id);

            return RedirectToAction("Index", "Product");
        }
    }
}
