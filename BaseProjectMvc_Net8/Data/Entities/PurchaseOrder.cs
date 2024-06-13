namespace BaseProjectMvc_Net8.Data.Entities
{
	public class PurchaseOrder
	{
		public int Id { get; set; }
		public int Product_Id { get; set; }
		public int Customer_Id { get; set; }
		public int Qty { get; set; }
		public int Total { get; set; }
	}
}
