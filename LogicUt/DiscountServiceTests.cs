using Common.Model;
using Logic.Services;
using LogicUt.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LogicUt
{
    [TestClass]
    public class DiscountServiceTests
    {
        DiscountService service;
        public DiscountServiceTests()
        {
            service = new DiscountService(new DiscountRepositoryMock(), null);
        }

        [TestMethod]
        public void AddDiscount_Null_ArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.AddDiscountAsync(null));
        }

        [TestMethod]
        public void AddDiscount_NotValid_ValidationException()
        {
            AuthorDiscount discount = new AuthorDiscount(new Author(),200);
            discount.AuthorId = 0;
            Assert.ThrowsExceptionAsync<ValidationException>(() => service.AddDiscountAsync(discount));
        }

        [TestMethod]
        public void AddDiscount_Valid()
        {
            BaseDiscount discount = new PublishDateDiscount(DateTime.Now, 50);

            var newDiscount = service.AddDiscountAsync(discount).Result;

            Assert.IsTrue(newDiscount.Id > 0);
        }

        [TestMethod]
        public void GetDiscounts()
        {
            var list = service.GetDiscountsAsync().Result;

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void RemoveDiscount_Null_NullArgumentException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.RemoveDiscountAsync(null));
        }

        [TestMethod]
        public void RemoveDiscount_Valid()
        {
            var discount = new PublisherDiscount(new Publisher { Id = 1 }, 50)
            {
                Id=2
            };

            service.RemoveDiscountAsync(discount).Wait();
        }

        [TestMethod]
        public void SetItemsPrices_Valid_ItemGotDiscount()
        {
            var book = new Book
            {
                Price = 100,
                Publisher = new Publisher { Id = 1 },
                PublisherId = 1
            };

            service.SetItemsPricesAsync(new List<AbstractItem> { book }).Wait();

            Assert.AreEqual(book.Price / 2, book.DiscountedPrice);
        }

        [TestMethod]
        public void SetItemsPrices_Valid_NoDiscountOnItem()
        {
            var book = new Book
            {
                Price = 100,
                Publisher = new Publisher { Id = 2 },
                PublisherId = 2
            };

            service.SetItemsPricesAsync(new List<AbstractItem> { book }).Wait();

            Assert.AreEqual(book.Price, book.DiscountedPrice);
        }
    }
}
