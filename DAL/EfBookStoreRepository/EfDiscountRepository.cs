using Common.Model;
using DAL.BookStoreRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EfBookStoreRepository
{
    public class EfDiscountRepository : IDiscountRepository
    {
        private readonly StoreContext context;
        public EfDiscountRepository(StoreContextFactory factory)
        {
            this.context = factory.GetContext();
        }
        public async Task<BaseDiscount> AddDiscountAsync(BaseDiscount discount)
        {
            if (discount == null)
                throw new ArgumentNullException();

            var newDiscount = await context.AddAsync(discount);
            await context.SaveChangesAsync();

            // this way we make sure that next time the discount is loaded its fresh from db and not cached
            newDiscount.State = EntityState.Detached;
            return newDiscount.Entity;
        }

        public Task<IEnumerable<BaseDiscount>> GetDiscountsAsync()
        {
            return Task.Run(() => context.Discounts.AsEnumerable());
        }

        public async Task RemoveDiscount(int discountId)
        {
            var discount = await context.Discounts.FirstOrDefaultAsync(d => d.Id == discountId);
            if (discount != null)
            {
                context.Discounts.Remove(discount);
                await context.SaveChangesAsync();
            }
        }
    }
}
