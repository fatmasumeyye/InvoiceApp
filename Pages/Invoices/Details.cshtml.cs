using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceApp.Pages.Invoices
{
    public class DetailsModel : PageModel
    {
        public ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Invoice Invoice { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            var invoice = _context.Invoices.Find(id);

            if (invoice == null)
            {
                return RedirectToPage("/Invoices/Index");
            }

            Invoice = invoice;

            return Page();
        }
    }
}