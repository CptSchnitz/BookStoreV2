using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.API
{
    public interface IGenreService
    {
        Task<List<Genre>> GetGenresAsync();

        Task<Genre> AddGenreAsync(Genre genre);
    }
}
