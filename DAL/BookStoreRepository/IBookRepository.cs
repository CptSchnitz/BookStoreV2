using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.BookStoreRepository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book book);
        Task<Book> EditBookAsync(Book book);
    }
}
