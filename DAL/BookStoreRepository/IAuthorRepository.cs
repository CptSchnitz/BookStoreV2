using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.BookStoreRepository
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task<Author> CreateAuthorAsync(Author author);
        Task<Author> EditAuthorAsync(Author author);
    }
}
