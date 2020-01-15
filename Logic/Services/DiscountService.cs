using Common.Model;
using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
                if (previousDiscount != null)
                    await discountRepo.RemoveDiscount(previousDiscount.Id);
                return await discountRepo.AddDiscountAsync(discount);
            }
            catch (DataException e)
            {
                logger.Error(e, "Error adding discount");
                throw new Exception("Error adding the discount to the db");
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
                logger.Error(e, "Error getting discounts");
                throw new Exception("Error getting discounts from the db");
            }
        }

        public async Task SetItemPriceAsync(AbstractItem item)
        {
            var discountList = await GetDiscountsAsync();
            SetItemPrice(item, discountList);
        }

        public async Task SetItemsPricesAsync(List<AbstractItem> itemList)
        {
            var discountList = await GetDiscountsAsync();
            itemList.ForEach(item => SetItemPrice(item, discountList));
        }

        private void SetItemPrice(AbstractItem item, List<BaseDiscount> discountList)
        {
            var discounts = discountList.Where(d => d.IsDiscountValid(item)).ToList();
            if (discounts.Count > 0)
            {
                var discount = discounts.Aggregate(discounts.First(),(maxD, d) => d.DiscountAmount > maxD.DiscountAmount ? d : maxD);
                item.DiscountedPrice = item.Price * discount.DiscountMulti;
                item.DiscountType = discount.GetType().Name;
            }
            else
            {
                item.DiscountedPrice = item.Price;
            }
        }
    }
}
