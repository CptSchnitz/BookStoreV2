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
    public class AuthorServiceTests
    {
        AuthorService service;
        public AuthorServiceTests()
        {
            service = new AuthorService(new AuthorRepositoryMock(), null);
        }

        [TestMethod]
        public void AddAuthor_Null_ArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.AddAuthorAsync(null));
        }

        [TestMethod]
        public void AddAuthor_NotValid_ValidationException()
        {
            Author author = new Author();
            Assert.ThrowsExceptionAsync<ValidationException>(() => service.AddAuthorAsync(author));
        }

        [TestMethod]
        public void AddAuthor_Valid()
        {
            Author author = new Author
            {
                FirstName = "c",
                LastName = "c",
            };

            var newAuthor = service.AddAuthorAsync(author).Result;

            Assert.IsTrue(newAuthor.Id > 0);            
        }

        [TestMethod]
        public void GetAuthors()
        {
            var list = service.GetAuthorsAsync().Result;

            Assert.IsNotNull(list);
        }
    }
}
