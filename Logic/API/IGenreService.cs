using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.API
{
    //service for all genre operations
    public interface IGenreService
    {
        Task<List<Genre>> GetGenresAsync();

        Task<Genre> AddGenreAsync(Genre genre);
    }
}
