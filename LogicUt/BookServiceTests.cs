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
    public class BookServiceTests
    {
        BookService service;
        public BookServiceTests()
        {
            service = new BookService(new BookRepositoryMock(), new DiscountServiceMock(), null);
        }

        [TestMethod]
        public void AddBook_Null_ArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.AddBookAsync(null));
        }

        [TestMethod]
        public void AddBook_NotValid_ValidationException()
        {
            Book book = new Book();
            Assert.ThrowsExceptionAsync<ValidationException>(() => service.AddBookAsync(book));
        }

        [TestMethod]
        public void AddBook_Valid()
        {
            Book book = new Book
            {
                DiscountedPrice = 15,
                Isbn = "sdsds",
                Title = "sss"
            };

            var newBook = service.AddBookAsync(book).Result;

            Assert.IsTrue(newBook.Id > 0);
        }

        [TestMethod]
        public void GetBooks()
        {
            var list = service.GetBooksAsync().Result;

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void EditBook_Null_ArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.UpdateBookAsync(null));
        }

        [TestMethod]
        public void EditBook_NotValid_ValidationException()
        {
            Book book = new Book();
            Assert.ThrowsExceptionAsync<ValidationException>(() => service.UpdateBookAsync(book));
        }

        [TestMethod]
        public void EditBook_DoesntExist_ArgumentException()
        {
            Book book = new Book
            {
                Id = 15,
                DiscountedPrice = 15,
                Isbn = "sdsds",
                Title = "sss"
            };
            Assert.ThrowsExceptionAsync<ValidationException>(() => service.UpdateBookAsync(book));
        }

        [TestMethod]
        public void EditBook_Valid()
        {
            Book book = new Book
            {
                Id = 1,
                DiscountedPrice = 15,
                Isbn = "sdsds",
                Title = "sss"
            };

            var newBook = service.AddBookAsync(book).Result;

            Assert.AreEqual(book.Title, newBook.Title);
        }
    }
}
