using Common.Model;
using DAL.BookStoreRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EfBookStoreRepository
{
    public class EfJournalRepository : IJournalRepository
    {
        private readonly StoreContext context;
        public EfJournalRepository(StoreContextFactory factory)
        {
            this.context = factory.GetContext();
        }
        public async Task<Journal> CreateJournalAsync(Journal journal)
        {
            if (journal == null)
                throw new ArgumentNullException();

            var newJournal = await context.Journals.AddAsync(journal);
            await context.SaveChangesAsync();
            return newJournal.Entity;
        }

        public async Task<Journal> EditJournalAsync(Journal journal)
        {
            if (journal == null)
                throw new ArgumentNullException();

            var journalToEdit = await GetJournalByIdAsync(journal.Id);
            context.Entry(journalToEdit).CurrentValues.SetValues(journal);
            await context.SaveChangesAsync();
            return journalToEdit;
        }

        public async Task<Journal> GetJournalByIdAsync(int id)
        {
            return await context
                .Journals
                .Include(j => j.Publisher)
                .Include(i => i.ItemGenres)
                .ThenInclude(ig => ig.Genre)
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public Task<IEnumerable<Journal>> GetJournalsAsync()
        {
            return Task.Run(() => context.Journals
                        .Include(j => j.Publisher)
                        .Include(i => i.ItemGenres)
                        .ThenInclude(ig => ig.Genre).AsEnumerable());
        }
    }
}
