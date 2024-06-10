using BaseProjectMvc_Net8.Data.Entities;

namespace BaseProjectMvc_Net8.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Task AddInventory(Inventory inventory);
        Task<IEnumerable<Inventory>> GetInventories();
        Task<Inventory> GetInventoryById(int? id);
        int GetStockByProductId(int? id);
        Task<Inventory> GetInventoryByProductId(int? id);
        Task UpdateInventory(Inventory inventory);
    }
}
