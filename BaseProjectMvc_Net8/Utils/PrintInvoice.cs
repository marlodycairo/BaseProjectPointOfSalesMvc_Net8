using BaseProjectMvc_Net8.Repositories.Interfaces;
using BaseProjectMvc_Net8.Utils.IUtils;
using QuestPDF.Fluent;

namespace BaseProjectMvc_Net8.Utils
{
    public class PrintInvoice(IInvoiceRepository invoiceRepository, IProductRepository productRepository) : IPrintInvoice
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<byte[]> PDF(int id)
        {
            var companyName = "Pharma Store";

            var invoice = await _invoiceRepository.GetInvoiceById(id);

            var data = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Margin(30);
                    page.Header().ShowOnce().Row(row =>
                    {
                        row.ConstantItem(140).Border(1).Column(col =>
                        {
                            col.Item().AlignCenter().Text($"{companyName}").Bold().FontSize(14);
                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Border(1).BorderColor("#257272").AlignCenter().Text($"Invoice No. {invoice.Invoice_Id}").Bold().FontSize(14);
                            col.Item().AlignCenter().Text($"Date Invoice {invoice.DateInvoice.Date}").FontSize(10);
                        });
                    });
                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().Table(async table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });
                            table.Header(header =>
                            {
                                header.Cell().Background("#257272").AlignCenter()
                                .Padding(2).Text("Product").FontColor("#fff");

                                header.Cell().Background("#257272").AlignCenter()
                                .Padding(2).Text("Price").FontColor("#fff");

                                header.Cell().Background("#257272").AlignCenter()
                                .Padding(2).Text("Quantity").FontColor("#fff");

                                header.Cell().Background("#257272").AlignCenter()
                                .Padding(2).Text("Subtotal").FontColor("#fff");
                            });
                            foreach (var item in invoice.Orders)
                            {
                                //var product = await _productRepository.GetProductById(item.Product_Id);
                                table.Cell().Text($"{item.Product.Product_Name}").FontSize(10);
                                table.Cell().AlignRight().Text($"{item.Product.Price}").FontSize(10);
                                table.Cell().AlignRight().Text($"{item.Quantity}").FontSize(10);
                                table.Cell().AlignRight().Text($"{item.Product.Price * item.Quantity}").FontSize(10);
                            }
                        });
                        col1.Spacing(5);
                        col1.Item().LineHorizontal(0.5f);
                        col1.Item().AlignRight().Text($"Total:         {invoice.Orders.Sum(x => x.Product.Price * x.Quantity)}").Bold().FontSize(12);
                    });
                });
            }).GeneratePdf();

            return data;
        }
    }
}
