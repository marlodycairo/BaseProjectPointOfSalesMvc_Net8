using BaseProjectMvc_Net8.Extensions;
using BaseProjectMvc_Net8.Models;

namespace BaseProjectMvc_Net8.Services
{
	public class OrderService : IOrderService
	{
		public ItemModel GetSubtotal(ItemModel itemModel, HttpContext? httpContext)
		{
			var items = httpContext?.Session.GetObjectFromJson<List<ItemModel>>("Items") ?? [];

			foreach (var item in items)
			{
				if (item.Id == itemModel.Id)
				{
					item.Quantity = itemModel.Quantity;
				}
			}

			return itemModel;
		}
	}
}
