using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using InvoiceApp.Models;
using InvoiceApp.Services;

namespace InvoiceApp.Pages_Invoices
{
    public class CreateModel : PageModel
    {
        private readonly InvoiceApp.Services.ApplicationDbContext _context;

        public CreateModel(InvoiceApp.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Invoices.Add(Invoice);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
