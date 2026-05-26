using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models
{
    public class InvoiceDto
    {
        [Required]
        public string? Number { get; set; } = "";

        [Required]
        public string? Status { get; set; } = "";

        public DateOnly? IssueDate { get; set; }

        public DateOnly? DueDate { get; set; }

        // Eski tek satırlı yapı için bırakıyoruz ama artık zorunlu değil
        public string? Service { get; set; } = "";

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        // İstemci ayrıntıları
        [Required(ErrorMessage = "Client name is required")]
        public string? ClientName { get; set; } = "";

        [Required, EmailAddress]
        public string? Email { get; set; } = "";

        [Phone]
        public string Phone { get; set; } = "";

        public string? Address { get; set; } = "";

        public List<InvoiceItemDto> Items { get; set; } = new()
        {
            new InvoiceItemDto()
        };
    }
}