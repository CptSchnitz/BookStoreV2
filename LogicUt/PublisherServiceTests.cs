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
    public class PublisherServiceTests
    {
        PublisherService service;
        public PublisherServiceTests()
        {
            service = new PublisherService(new PublisherRepositoryMock(), null);
        }

        [TestMethod]
        public void AddAuthor_Null_ArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.AddPublisherAsync(null));
        }

        [TestMethod]
        public void AddPublisher_NotValid_ValidationException()
        {
            Publisher publisher = new Publisher();
            Assert.ThrowsExceptionAsync<ValidationException>(() => service.AddPublisherAsync(publisher));
        }

        [TestMethod]
        public void AddPublisher_Valid()
        {
            Publisher publisher = new Publisher
            {
                Name = "c",
                ContactEmail = "c@c.com"
            };

            var newPublisher = service.AddPublisherAsync(publisher).Result;

            Assert.IsTrue(newPublisher.Id > 0);
        }

        [TestMethod]
        public void GetPublishers()
        {
            var list = service.GetPublishersAsync().Result;

            Assert.IsNotNull(list);
        }
    }
}
