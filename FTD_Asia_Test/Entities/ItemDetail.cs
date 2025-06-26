using System.ComponentModel.DataAnnotations;

namespace FTD_Asia_Test.Entities
{
    public class ItemDetail
    {
        [Required(ErrorMessage = "PartnerItemRef is required.")]
        [MaxLength(50)]
        public string PartnerItemRef { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 5, ErrorMessage = "Quantity must be between 1 and 5.")]
        public int Qty { get; set; }

        [Required(ErrorMessage = "UnitPrice is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "UnitPrice must be a positive value in cents.")]
        public long UnitPrice { get; set; } 
    }
}
