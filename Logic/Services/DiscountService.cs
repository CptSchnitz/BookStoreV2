using Common.Model;
using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class DiscountService : ServiceBase, IDiscountService
    {
        IDiscountRepository discountRepo;
        public DiscountService(ILogger logger, IDiscountRepository discountRepo) : base(logger)
        {
            this.discountRepo = discountRepo;
        }

        public async Task<BaseDiscount> AddDiscountAsync(BaseDiscount discount)
        {
            try
            {
                var previousDiscount = (await discountRepo.GetDiscountsAsync()).FirstOrDefault(d => d.IsSameDiscount(discount));

                // remove the old discount with the same value
                if (previousDiscount != null)
                    await discountRepo.RemoveDiscount(previousDiscount.Id);

                return await discountRepo.AddDiscountAsync(discount);
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error adding discount");
                throw new DataException("Error adding the discount to the db");
            }
        }

        public async Task<List<BaseDiscount>> GetDiscountsAsync()
        {
            try
            {
                return (await discountRepo.GetDiscountsAsync()).ToList();
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error getting discounts");
                throw new DataException("Error getting discounts from the db");
            }
        }

        public async Task RemoveDiscountAsync(BaseDiscount discount)
        {
            try
            {
                await discountRepo.RemoveDiscount(discount.Id);
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error removing discount");
                throw new DataException("Error removing discount from the db");
            }
        }

        // set the price after discount for item
        public async Task SetItemPriceAsync(AbstractItem item)
        {
            var discountList = await GetDiscountsAsync();
            SetItemPrice(item, discountList);
        }

        // set the price after discount for item enumerable
        public async Task SetItemsPricesAsync(IEnumerable<AbstractItem> itemList)
        {
            var discountList = await GetDiscountsAsync();
            foreach (var item in itemList)
            {
                SetItemPrice(item, discountList);
            }
        }

        private void SetItemPrice(AbstractItem item, List<BaseDiscount> discountList)
        {
            // gets a list with all the discounts that are applicable for the item
            var discounts = discountList.Where(d => d.IsDiscountValid(item)).ToList();
            
            // checks if there is any discount
            if (discounts.Count > 0)
            {
                // finds the highest discount
                var discount = discounts.Aggregate(discounts.First(), (maxD, d) => d.DiscountAmount > maxD.DiscountAmount ? d : maxD);

                item.DiscountedPrice = item.Price * discount.DiscountMulti;
                item.DiscountType = discount.Discriminator;
            }
            else
            {
                item.DiscountedPrice = item.Price;
            }
        }
    }
}
