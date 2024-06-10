using BaseProjectMvc_Net8.Data.Entities;

namespace BaseProjectMvc_Net8.Repositories.Interfaces
{
	public interface IInvoiceRepository
	{
		Task<Invoice> AddInvoice(Invoice invoice);
		Task<IEnumerable<Invoice>> GetInvoices();
		Task<Invoice> GetInvoiceById(int id);
	}
}
