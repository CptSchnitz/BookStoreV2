using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.API
{
    // service for all books operations
    public interface IBookService
    {
        Task<List<Book>> GetBooksAsync();
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
    }
}
