using Common.Model;
using DAL.BookStoreRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
            return await context.Discounts.FirstOrDefaultAsync(d => d.Id == newDiscount.Entity.Id);
        }

        public  async Task<IEnumerable<BaseDiscount>> GetDiscountsAsync()
        {
            return await context.Discounts.ToListAsync();
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
