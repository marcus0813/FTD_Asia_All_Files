using FTD_Asia_API.Entities;
using System.ComponentModel.DataAnnotations;

namespace FTD_Asia_API.Models.Request
{
    public class PartnerRequest
    {
        [Required]
        [MaxLength(50)]
        public string PartnerKey { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string PartnerRefNo { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string PartnerPassword { get; set; } = string.Empty;

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "TotalAmount must be a positive value (in cents).")]
        public long TotalAmount { get; set; }

        public List<ItemDetail>? Items { get; set; }

        [Required]
        public string Timestamp { get; set; } = string.Empty; 

        [Required]
        public string Sig { get; set; } = string.Empty;
    }
}