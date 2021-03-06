﻿using Common.Model;
using DAL.BookStoreRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EfBookStoreRepository
{
    public class EfGenreRepository : IGenreRepository
    {
        private readonly StoreContext context;
        public EfGenreRepository(StoreContextFactory factory)
        {
            this.context = factory.GetContext();
        }
        public async Task<Genre> CreateGenreAsync(Genre genre)
        {
            if (genre == null)
                throw new ArgumentNullException();

            var newGenre = await context.AddAsync(genre);
            await context.SaveChangesAsync();
            newGenre.State = EntityState.Detached;
            return newGenre.Entity;
        }

        public async Task<Genre> EditGenreAsync(Genre genre)
        {
            if (genre == null)
                throw new ArgumentNullException();

            var genreToEdit = await GetGenreByIdAsync(genre.Id);
            context.Entry(genreToEdit).CurrentValues.SetValues(genre);

            await context.SaveChangesAsync();
            return genreToEdit;

        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await context.Genres.SingleOrDefaultAsync(a => a.Id == id);
        }

        public Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return Task.Run(() => context.Genres.AsEnumerable());
        }
    }
}
