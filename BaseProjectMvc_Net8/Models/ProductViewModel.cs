using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Models.Response;

namespace BaseProjectMvc_Net8.Models
{
    public class ProductViewModel
    {
        public List<Product>? Products { get; set; }
        public ItemModel? ItemSelected { get; set; }
        public List<ItemModel>? ItemsSelected { get; set; }
        public List<ItemModel>? ProductsList { get; set; }
    }
}
