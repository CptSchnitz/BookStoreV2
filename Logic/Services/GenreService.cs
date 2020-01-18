using Common.Model;
using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class GenreService : ServiceBase, IGenreService
    {
        IGenreRepository genreRepository;
        public GenreService(IGenreRepository genreRepository, ILogger logger) : base(logger)
        {
            this.genreRepository = genreRepository;
        }

        public async Task<Genre> AddGenreAsync(Genre genre)
        {
            if (genre is null)
            {
                throw new System.ArgumentNullException(nameof(genre));
            }

            Validator.ValidateObject(genre, new ValidationContext(genre));


            try
            {
                var newGenre = await genreRepository.CreateGenreAsync(genre);
                return newGenre;
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error Adding new Genre");
                throw new DataException("Error Adding genre to db");
            }
        }

        public async Task<List<Genre>> GetGenresAsync()
        {
            try
            {
                var genreList = (await genreRepository.GetGenresAsync()).ToList();
                return genreList;
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error loading Genres");
                throw new DataException("Error getting data from db");
            }

        }
    }
}
