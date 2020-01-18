using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.API
{
    //service for discounts
    public interface IDiscountService
    {
        Task<List<BaseDiscount>> GetDiscountsAsync();
        // adds discount, if the same discount exists (with different %) it replaces it.
        Task<BaseDiscount> AddDiscountAsync(BaseDiscount discount);

        Task RemoveDiscountAsync(BaseDiscount discount);

        // sets the price after discount for all the items in itelList
        Task SetItemsPricesAsync(IEnumerable<AbstractItem> itemList);

    }
}
