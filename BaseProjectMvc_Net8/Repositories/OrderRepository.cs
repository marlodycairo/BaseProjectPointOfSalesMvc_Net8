using BaseProjectMvc_Net8.Data.Context;
using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectMvc_Net8.Repositories
{
	public class OrderRepository(ApplicationDbContext context) : IOrderRepository
	{
		private readonly ApplicationDbContext _context = context;

		public async Task<Order> CreateOrder(Order order)
		{
			await _context.Orders.AddAsync(order);

			await _context.SaveChangesAsync();

			return order;
		}

		public async Task<IEnumerable<Order>> GetOrders()
		{
			return await _context.Orders.ToListAsync();
		}
	}
}
