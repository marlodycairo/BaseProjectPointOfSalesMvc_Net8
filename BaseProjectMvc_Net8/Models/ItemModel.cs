namespace BaseProjectMvc_Net8.Models
{
    public class ItemModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Product_Id { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public decimal Subtotal
        {
            get
            {
                return Price * Quantity;
            }
        }

        public string Product_Name { get; set; } = string.Empty;

        public int StockProducts { get; set; }

        public decimal Total { get; set; }
    }
}
