using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Models;
using InvoiceApp.Services;

namespace InvoiceApp.Pages_Invoices
{
    public class DetailsModel : PageModel
    {
        private readonly InvoiceApp.Services.ApplicationDbContext _context;

        public DetailsModel(InvoiceApp.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public Invoice Invoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FirstOrDefaultAsync(m => m.Id == id);

            if (invoice is not null)
            {
                Invoice = invoice;

                return Page();
            }

            return NotFound();
        }
    }
}
