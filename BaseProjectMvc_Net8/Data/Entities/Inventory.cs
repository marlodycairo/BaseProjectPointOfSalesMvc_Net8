using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseProjectMvc_Net8.Data.Entities
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public int Stock { get; set; }

        [ForeignKey("Product_Id")]
        public Product? Product { get; set; }
    }
}
