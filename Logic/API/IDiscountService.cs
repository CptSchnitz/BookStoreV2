using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.API
{
    public interface IDiscountService
    {
        Task<List<BaseDiscount>> GetDiscountsAsync();
        Task<BaseDiscount> AddDiscountAsync(BaseDiscount discount);

        Task RemoveDiscountAsync(BaseDiscount discount);

        Task SetItemsPricesAsync(List<AbstractItem> itemList);

        Task SetItemPriceAsync(AbstractItem item);

    }
}
