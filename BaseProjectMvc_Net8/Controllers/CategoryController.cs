using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectMvc_Net8.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryRepository.GetCategories();

            return View(result.Result);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            await _categoryRepository.AddCategory(category);

            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _categoryRepository.GetCategoryById(id);

            return View(result.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _categoryRepository.GetCategoryById(id);

            return View(result.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            await _categoryRepository.UpdateCategory(category);

            var categoryUpdated = new Category();

            var response = await _categoryRepository.GetCategoryById(category.Id);

            categoryUpdated = response.Result;

            return View("Details", categoryUpdated);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = new Category();

            var response = await _categoryRepository.GetCategoryById(id);

            category = response.Result;

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryRepository.DeleteCategory(id);

            return RedirectToAction("Index", "Category");
        }
    }
}
