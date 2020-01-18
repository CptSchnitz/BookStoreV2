using Common.Model;
using DAL.BookStoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicUt.Mocks
{
    class BookRepositoryMock : IBookRepository
    {
        private List<Book> books = new List<Book>()
        {
            new Book
            {
                Id = 1,
                AmountInStock = 5,
                Author = new Author
                {
                    Id = 1,
                    FirstName = "a",
                    LastName = "a"
                },
                AuthorId = 1,
                Description = "a",
                Edition = 1,
                Isbn = "aaaa",
                ItemGenres = new List<ItemGenre>()
                {
                    new ItemGenre
                    {
                        AbstractItemId=1,
                        Genre = new Genre {Id = 1, Name="test"},
                        GenreId = 1
                    }
                },
                Price = 100m,
                PublishDate = new DateTime(2000,1,1),
                Title = "A",
                Publisher = new Publisher{Id = 1, Name ="a",ContactEmail="a@a.com"},
                PublisherId = 1
            },
        };

        public Task<Book> CreateBookAsync(Book book)
        {
            book.Id = books.Last().Id + 1;
            books.Add(book);
            return Task.Run(() => book);
        }

        public Task<Book> EditBookAsync(Book book)
        {
            var index = books.FindIndex(a => a.Id == book.Id);
            books[index] = book;
            return Task.Run(() => book);
        }

        public Task<Book> GetBookByIdAsync(int id)
        {
            return Task.Run(() => books.FirstOrDefault(a => a.Id == id));
        }

        public Task<IEnumerable<Book>> GetBooksAsync()
        {
            return Task.Run(() => books.AsEnumerable());
        }
    }
}
