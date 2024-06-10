namespace BaseProjectMvc_Net8.Data.Entities
{
	public class Invoice
	{
		public int Id { get; set; }
		public int PurchaseOrder { get; set; }
		public int Customer_Id { get; set; }
		public decimal Total { get; set; }
	}
}
