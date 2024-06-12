using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseProjectMvc_Net8.Data.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Product_Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Category_Id { get; set; }

        [ForeignKey("Category_Id")]
        public Category Category { get; set; }
    }
}
