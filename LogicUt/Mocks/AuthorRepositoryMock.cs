using Common.Model;
using DAL.BookStoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicUt.Mocks
{
    class AuthorRepositoryMock : IAuthorRepository
    {
        private List<Author> authors = new List<Author>()
        {
            new Author
            {
                Id = 1,
                FirstName = "a",
                LastName = "a",
                PseuduName = "A"
            },
            new Author
            {
                Id = 2,
                FirstName = "b",
                LastName = "b",
                PseuduName = "B"
            }
        };

        public Task<Author> CreateAuthorAsync(Author author)
        {
            author.Id = authors.Last().Id + 1;
            authors.Add(author);
            return Task.Run(() => author);
        }

        public Task<Author> EditAuthorAsync(Author author)
        {
            var index = authors.FindIndex(a => a.Id == author.Id);
            authors[index] = author;
            return Task.Run(() => author);
        }

        public Task<Author> GetAuthorByIdAsync(int id)
        {
            return Task.Run(() => authors.FirstOrDefault(a => a.Id == id));
        }

        public Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return Task.Run(() => authors.AsEnumerable());
        }
    }
}
