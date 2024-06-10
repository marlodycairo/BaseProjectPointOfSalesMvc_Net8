using BaseProjectMvc_Net8.Data.Context;
using BaseProjectMvc_Net8.Data.Entities;
using BaseProjectMvc_Net8.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectMvc_Net8.Repositories
{
	public class InvoiceRepository(ApplicationDbContext context) : IInvoiceRepository
	{
		private readonly ApplicationDbContext _context = context;

		public async Task<Invoice> AddInvoice(Invoice invoice)
		{
			await _context.Invoices.AddAsync(invoice);

			await _context.SaveChangesAsync();

			return invoice;
		}

		public async Task<Invoice> GetInvoiceById(int id)
		{
			var invoice = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == id);

			return invoice!;
		}

		public async Task<IEnumerable<Invoice>> GetInvoices()
		{
			return await _context.Invoices.ToListAsync();
		}
	}
}
