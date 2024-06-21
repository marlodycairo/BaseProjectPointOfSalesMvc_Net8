using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseProjectMvc_Net8.Data.Entities
{
	public class Order
	{
		[Key]
		public int Order_Id { get; set; }
		public int Product_Id { get; set; }
		public int Quantity { get; set; }
		public DateTime DateOrder { get; set; }
		public int Invoice_Id { get; set; }

        [ForeignKey("Product_Id")]
		public Product Product { get; set; }

		[ForeignKey("Invoice_Id")]
		public Invoice Invoice { get; set; }
	}
}
