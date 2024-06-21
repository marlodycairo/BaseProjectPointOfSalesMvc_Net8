using BaseProjectMvc_Net8.Data.Entities;

namespace BaseProjectMvc_Net8.Repositories.Interfaces
{
	public interface IOrderRepository
	{
		Task<Order> CreateOrder(Order order);
		Task<IEnumerable<Order>> GetOrders();
	}
}
