using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseProjectMvc_Net8.Data.Entities
{
	public class Invoice
	{
		[Key]
		public int Invoice_Id { get; set; }
		public DateTime DateInvoice { get; set; }
		public int Customer_Id { get; set; }

		[ForeignKey("Customer_Id")]
		public User User { get; set; }

		public List<Order> Orders { get; set; } = [];
    }
}
