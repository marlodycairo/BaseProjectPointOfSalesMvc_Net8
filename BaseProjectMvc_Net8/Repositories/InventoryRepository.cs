using BaseProjectMvc_Net8.Data.Context;
using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectMvc_Net8.Repositories
{
    public class InventoryRepository(ApplicationDbContext context) : IInventoryRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddInventory(Inventory inventory)
        {
            await _context.Inventories.AddAsync(inventory);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetInventories()
        {
            return await _context.Inventories.Include(p => p.Product)
                .ThenInclude(x => x.Category).ToListAsync();
        }

        public async Task<Inventory> GetInventoryById(int? id)
        {
            var inventory = await _context.Inventories.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);

            return inventory!;
        }

		public async Task<Inventory> GetInventoryByProductId(int? id)
		{
            var product = await _context.Inventories.Include(p => p.Product)
                .ThenInclude(x => x.Category).FirstOrDefaultAsync(x => x.Product_Id == id);

            return product!;
		}

		public int GetStockByProductId(int? id)
        {
            var stock = _context.Inventories.FirstOrDefault(x => x.Product_Id == id)?.Stock;

            return stock!.Value;
        }

		public async Task UpdateInventory(Inventory inventory)
		{
            var inventoryUpdated = new Inventory
            {
                Id = inventory.Id,
                Product_Id = inventory.Product_Id,
                Stock = inventory.Stock
            };

            _context.ChangeTracker.Clear();

			_context.Inventories.Update(inventoryUpdated);

            await _context.SaveChangesAsync();
		}
	}
}
