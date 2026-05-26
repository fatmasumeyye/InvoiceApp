using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceApp.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public List<Invoice> invoiceList { get; set; } = new();

        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;

        public List<int> PageSizeOptions { get; set; } = new() { 5, 10, 20, 50 };

        public IndexModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet(int pageIndex = 1, int pageSize = 10)
        {
            if (!PageSizeOptions.Contains(pageSize))
            {
                pageSize = 10;
            }

            PageSize = pageSize;

            var query = context.Invoices
                .OrderByDescending(i => i.Id);

            int totalRecords = query.Count();

            TotalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            if (TotalPages == 0)
            {
                TotalPages = 1;
            }

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            if (pageIndex > TotalPages)
            {
                pageIndex = TotalPages;
            }

            PageIndex = pageIndex;

            invoiceList = query
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}