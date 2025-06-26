using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FTD_Asia_Test.Models.Response
{
    public class PartnerResponse
    {
        public int Result { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? TotalAmount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? TotalDiscount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? FinalAmount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ResultMessage { get; set; }
    }
}
