using Common.Model;
using DAL.BookStoreRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DAL.EfBookStoreRepository
{
    public class EfAuthorRepository : IAuthorRepository
    {
        private readonly StoreContext context;
        public EfAuthorRepository(StoreContextFactory factory)
        {
            this.context = factory.GetContext();
        }
        public async Task<Author> CreateAuthorAsync(Author author)
        {
            if (author == null)
                throw new ArgumentNullException();

            var newAuthor = await context.AddAsync(author);
            await context.SaveChangesAsync();
            return newAuthor.Entity;
        }

        public async Task<Author> EditAuthorAsync(Author author)
        {
            if (author == null)
                throw new ArgumentNullException();

            var authorToEdit = await GetAuthorByIdAsync(author.Id);
            context.Entry(authorToEdit).CurrentValues.SetValues(author);
            await context.SaveChangesAsync();
            return authorToEdit;

        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await context.Authors.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await context.Authors.ToListAsync();
        }
    }
}
