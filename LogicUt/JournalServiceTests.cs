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
    public class JournalServiceTests
    {
        JournalService service;
        public JournalServiceTests()
        {
            service = new JournalService(new JournalRepositoryMock(), new DiscountServiceMock(), null);
        }

        [TestMethod]
        public void AddJournal_Null_ArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.AddJournalAsync(null));
        }

        [TestMethod]
        public void AddJournal_NotValid_ValidationException()
        {
            Journal journal = new Journal();
            Assert.ThrowsExceptionAsync<ValidationException>(() => service.AddJournalAsync(journal));
        }

        [TestMethod]
        public void AddJournal_Valid()
        {
            Journal journal = new Journal
            {
                DiscountedPrice = 15,
                Issn = "sdsds",
                Title = "sss"
            };

            var newJournal = service.AddJournalAsync(journal).Result;

            Assert.IsTrue(newJournal.Id > 0);
        }

        [TestMethod]
        public void GetJournals()
        {
            var list = service.GetJournalsAsync().Result;

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void EditJournal_Null_ArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.UpdateJournalAsync(null));
        }

        [TestMethod]
        public void EditJournal_NotValid_ValidationException()
        {
            Journal journal = new Journal();
            Assert.ThrowsExceptionAsync<ValidationException>(() => service.UpdateJournalAsync(journal));
        }

        [TestMethod]
        public void EditJournal_DoesntExist_ArgumentException()
        {
            Journal journal = new Journal
            {
                Id = 15,
                DiscountedPrice = 15,
                Issn = "sdsds",
                Title = "sss"
            };
            Assert.ThrowsExceptionAsync<ValidationException>(() => service.UpdateJournalAsync(journal));
        }

        [TestMethod]
        public void EditJournal_Valid()
        {
            Journal journal = new Journal
            {
                Id = 1,
                DiscountedPrice = 15,
                Issn = "sdsds",
                Title = "sss"
            };

            var newJournal = service.AddJournalAsync(journal).Result;

            Assert.AreEqual(journal.Title, newJournal.Title);
        }
    }
}
