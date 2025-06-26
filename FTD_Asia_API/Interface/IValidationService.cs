using FTD_Asia_API.Entities;
using FTD_Asia_API.Models.Request;

namespace FTD_Asia_API.Interface
{
    public interface IValidationService
    {
        bool IsAuthorizedUser(PartnerRequest request);
        bool IsValidTotalAmount(long totalAmount, List<ItemDetail> itemDetails);
        bool IsValidTimestamp(string timeStamp);
    }
}
