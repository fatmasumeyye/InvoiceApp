using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models
{
    public class InvoiceItemDto
    {
        [Required]
        public string? Service { get; set; } = "";

        [Range(1, 99, ErrorMessage = "Quantity is not valid")]
        public int Quantity { get; set; } = 1;

        [Range(1, 999999, ErrorMessage = "Unit price is not valid")]
        public decimal UnitPrice { get; set; }
    }
}