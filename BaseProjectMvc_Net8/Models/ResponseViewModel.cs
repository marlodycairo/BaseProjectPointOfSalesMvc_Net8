using BaseProjectMvc_Net8.Data.Entities;

namespace BaseProjectMvc_Net8.Models
{
	public class ResponseViewModel
	{
		public List<Product> Products { get; set; }
		public ItemModel ItemSelected { get; set; }
		public List<ItemModel> ItemsSelected { get; set; }
		public List<ItemModel> ProductsList { get; set; }
		public List<Category> Categories { get; set; }
		public List<User> Users { get; set; }
		public List<Order> OrdersInvoice { get; set; }
	}
}
