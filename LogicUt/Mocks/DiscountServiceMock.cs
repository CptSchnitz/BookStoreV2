using Common.Model;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicUt.Mocks
{
    class DiscountServiceMock : IDiscountService
    {
        public Task<BaseDiscount> AddDiscountAsync(BaseDiscount discount)
        {
            throw new NotImplementedException();
        }

        public Task<List<BaseDiscount>> GetDiscountsAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveDiscountAsync(BaseDiscount discount)
        {
            throw new NotImplementedException();
        }

        public Task SetItemsPricesAsync(IEnumerable<AbstractItem> itemList)
        {
            return Task.Run(() =>
            {
                foreach (var item in itemList)
                {
                    if (item.PublisherId == 1)
                    {
                        item.DiscountedPrice = item.Price / 2;
                    }
                    else
                    {
                        item.DiscountedPrice = item.Price;
                    }
                }
            });
        }
    }
}
