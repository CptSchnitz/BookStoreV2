using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.BookStoreRepository
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetGenresAsync();
        Task<Genre> GetGenreByIdAsync(int id);
        Task<Genre> CreateGenreAsync(Genre genre);
        Task<Genre> EditGenreAsync(Genre genre);
    }
}
