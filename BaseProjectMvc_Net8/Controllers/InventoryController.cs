using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BaseProjectMvc_Net8.Controllers
{
    public class InventoryController(IInventoryRepository inventoryRepository) : Controller
    {
        private readonly IInventoryRepository _inventoryRepository = inventoryRepository;

        public async Task<IActionResult> Index()
        {
            var result = await _inventoryRepository.GetInventories();

            return View(result);
        }

        [HttpGet]
        public IActionResult CreateInventory() { return View(); }

        [HttpPost]
        public async Task<IActionResult> CreateInventory(Inventory inventory)
        {
            await _inventoryRepository.AddInventory(inventory);

            var inventories = await _inventoryRepository.GetInventories();

            return View("Index", inventories);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _inventoryRepository.GetInventoryById(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Inventory inventory)
        {
            await _inventoryRepository.UpdateInventory(inventory);

            var inventories = await _inventoryRepository.GetInventories();

            return View("Index", inventories);
        }
    }
}
