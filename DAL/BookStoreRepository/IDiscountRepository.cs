using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.BookStoreRepository
{
    public interface IDiscountRepository
    {
        Task<BaseDiscount> AddDiscountAsync(BaseDiscount discount);

        Task<IEnumerable<BaseDiscount>> GetDiscountsAsync();

        Task RemoveDiscount(int discountId);
    }
}
