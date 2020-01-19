using Common.Model;
using DAL.BookStoreRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EfBookStoreRepository
{
    public class EfBookRepository : IBookRepository
    {
        private readonly StoreContext context;
        public EfBookRepository(StoreContextFactory factory)
        {
            this.context = factory.GetContext();
        }
        public async Task<Book> CreateBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            var newBook = await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return newBook.Entity;
        }

        public async Task<Book> EditBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();

            var bookToEdit = await GetBookByIdAsync(book.Id);
            context.Entry(bookToEdit).CurrentValues.SetValues(book);

            await context.SaveChangesAsync();
            return bookToEdit;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await context
                .Books
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(i => i.ItemGenres)
                .ThenInclude(ig => ig.Genre)
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public Task<IEnumerable<Book>> GetBooksAsync()
        {
            return Task.Run(() => context.Books
                    .Include(b => b.Publisher)
                    .Include(b => b.Author)
                    .Include(i => i.ItemGenres)
                    .ThenInclude(ig => ig.Genre).AsEnumerable());
        }
    }
}
