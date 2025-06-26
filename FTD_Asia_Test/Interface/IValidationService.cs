using FTD_Asia_Test.Entities;
using FTD_Asia_Test.Models.Request;

namespace FTD_Asia_Test.Interface
{
    public interface IValidationService
    {
        bool IsAuthorizedUser(PartnerRequest request);
        bool IsValidTotalAmount(long totalAmount, List<ItemDetail> itemDetails);
        bool IsValidTimestamp(string timeStamp);
    }
}
