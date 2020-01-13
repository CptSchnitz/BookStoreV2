using DAL.BookStoreRepository;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class GenreService : IGenreService
    {
        IGenreRepository genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }
    }
}
