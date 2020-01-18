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
    public class GenreServiceTests
    {
        GenreService service;
        public GenreServiceTests()
        {
            service = new GenreService(new GenreRepositoryMock(), null);
        }

        [TestMethod]
        public void AddGenre_Null_ArgumentNullException()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.AddGenreAsync(null));
        }

        [TestMethod]
        public void AddGenre_NotValid_ValidationException()
        {
            Genre genre = new Genre();
            Assert.ThrowsExceptionAsync<ValidationException>(() => service.AddGenreAsync(genre));
        }

        [TestMethod]
        public void AddGenre_Valid()
        {
            Genre genre = new Genre
            {
                Name = "c"
            };

            var newAuthor = service.AddGenreAsync(genre).Result;

            Assert.IsTrue(newAuthor.Id > 0);
        }

        [TestMethod]
        public void GetAuthors()
        {
            var list = service.GetGenresAsync().Result;

            Assert.IsNotNull(list);
        }
    }
}
