using FTD_Asia_API.Entities;

namespace FTD_Asia_API.Interface
{
    public interface IDiscountRulesService
    {
        DiscountedItem GetDiscountedItem(long totalAmount);
    }
}
