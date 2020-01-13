﻿using Common.Model;
using DAL.BookStoreRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EfBookStoreRepository
{
    class EfItemRepository : IItemRepository
    {
        private readonly StoreContext context;
        public EfItemRepository(StoreContext context)
        {
            this.context = context;
        }
        public async Task<AbstractItem> GetAbstractItemByIdAsync(int id)
        {
            return await context.Items
                .Include(i => i.ItemGenres)
                .ThenInclude(ig => ig.Genre)               
                .SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<AbstractItem>> GetAbstractItemsAsync()
        {
            return await context.Items
                .Include(i => i.Publisher)
                .Include(i => i.ItemGenres)
                .ThenInclude(ig => ig.Genre)
                .ToListAsync();
        }
    }
}
