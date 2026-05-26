using InvoiceApp.Models;
using InvoiceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceApp.Pages.Invoices
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InvoiceDto InvoiceDto { get; set; } = new();

        public void OnGet()
        {
            if (InvoiceDto.Items.Count == 0)
            {
                InvoiceDto.Items.Add(new InvoiceItemDto());
            }
        }

        public IActionResult OnPost()
        {
            InvoiceDto.Items = InvoiceDto.Items
                .Where(i => !string.IsNullOrWhiteSpace(i.Service))
                .ToList();

            if (InvoiceDto.Items.Count == 0)
            {
                ModelState.AddModelError("", "At least one invoice item is required.");
                InvoiceDto.Items.Add(new InvoiceItemDto());
                return Page();
            }

            if (!ModelState.IsValid)
            {
                if (InvoiceDto.Items.Count == 0)
                {
                    InvoiceDto.Items.Add(new InvoiceItemDto());
                }

                return Page();
            }

            decimal grandTotal = InvoiceDto.Items.Sum(i => i.Quantity * i.UnitPrice);

            Invoice invoice = new()
            {
                Number = InvoiceDto.Number,
                Status = InvoiceDto.Status,
                IssueDate = InvoiceDto.IssueDate,
                DueDate = InvoiceDto.DueDate,
                ClientName = InvoiceDto.ClientName,
                Email = InvoiceDto.Email,
                Phone = InvoiceDto.Phone,
                Address = InvoiceDto.Address,

                // Eski liste ekranı bozulmasın diye özet bilgi tutuyoruz
                Service = InvoiceDto.Items.Count == 1
                    ? InvoiceDto.Items[0].Service
                    : $"{InvoiceDto.Items.Count} invoice items",

                Quantity = 1,
                UnitPrice = grandTotal,

                InvoiceItems = InvoiceDto.Items.Select(item => new InvoiceItem
                {
                    Service = item.Service,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return RedirectToPage("/Invoices/Index");
        }
    }
}