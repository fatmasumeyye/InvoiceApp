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
    public class IndexModel : PageModel
    {
        private readonly InvoiceApp.Services.ApplicationDbContext _context;

        public IndexModel(InvoiceApp.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Invoice> Invoice { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Invoice = await _context.Invoices.ToListAsync();
        }
    }
}
