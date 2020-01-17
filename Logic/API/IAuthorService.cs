using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.API
{
    // service for all author operations
    public interface IAuthorService
    {
        Task<Author> AddAuthorAsync(Author author);
        Task<List<Author>> GetAuthorsAsync();
    }
}
