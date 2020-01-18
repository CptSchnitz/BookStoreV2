using Common.Model;
using DAL.BookStoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicUt.Mocks
{
    class GenreRepositoryMock : IGenreRepository
    {
        private List<Genre> genres = new List<Genre>()
        {
            new Genre
            {
                Id = 1,
                Name = "a",
            },
            new Genre
            {
                Id = 2,
                Name = "b"
            }
        };

        public Task<Genre> CreateGenreAsync(Genre genre)
        {
            genre.Id = genres.Last().Id + 1;
            genres.Add(genre);
            return Task.Run(() => genre);
        }

        public Task<Genre> EditGenreAsync(Genre genre)
        {
            var index = genres.FindIndex(a => a.Id == genre.Id);
            genres[index] = genre;
            return Task.Run(() => genre);
        }

        public Task<Genre> GetGenreByIdAsync(int id)
        {
            return Task.Run(() => genres.FirstOrDefault(a => a.Id == id));
        }

        public Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return Task.Run(() => genres.AsEnumerable());
        }
    }
}
