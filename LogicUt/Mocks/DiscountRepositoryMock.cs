using Common.Model;
using DAL.BookStoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicUt.Mocks
{
    class DiscountRepositoryMock : IDiscountRepository
    {
        private List<BaseDiscount> discounts = new List<BaseDiscount>()
        {
            new PublisherDiscount(new Publisher{Id = 1, Name ="a",ContactEmail="a@a.com"},50)
            {
                Id =1,
                PublisherId = 1
                
            },
            new AuthorDiscount(new Author
                {
                    Id = 1,
                    FirstName = "a",
                    LastName = "a"
            }, 50)
            {
                Id = 2,
                AuthorId = 1
                
            }
        };
        public Task<BaseDiscount> AddDiscountAsync(BaseDiscount discount)
        {
            discount.Id = discounts.Last().Id + 1;
            discounts.Add(discount);
            return Task.Run(() => discount); ;
        }

        public Task<IEnumerable<BaseDiscount>> GetDiscountsAsync()
        {
            return Task.Run(() => discounts.AsEnumerable());
        }

        public Task RemoveDiscount(int discountId)
        {
            return Task.Run(() =>
            {
                var itemToRemove = discounts.First(d => d.Id == discountId);
                discounts.Remove(itemToRemove);
            });
        }
    }
}
