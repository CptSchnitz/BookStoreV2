using Common.Model;
using DAL.BookStoreRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EfBookStoreRepository
{
    public class EfPublisherRepository : IPublisherRepository
    {
        private readonly StoreContext context;
        public EfPublisherRepository(StoreContextFactory factory)
        {
            this.context = factory.GetContext();
        }
        public async Task<Publisher> CreatePublisherAsync(Publisher publisher)
        {
            if (publisher == null)
                throw new ArgumentNullException();

            var newPublisher = await context.AddAsync(publisher);
            await context.SaveChangesAsync();
            return newPublisher.Entity;
        }

        public async Task<Publisher> EditPublisherAsync(Publisher publisher)
        {
            if (publisher == null)
                throw new ArgumentNullException();

            var publisherToEdit = await GetPublisherByIdAsync(publisher.Id);
            context.Entry(publisherToEdit).CurrentValues.SetValues(publisher);
            await context.SaveChangesAsync();
            return publisherToEdit;

        }

        public async Task<Publisher> GetPublisherByIdAsync(int id)
        {
            return await context.Publishers.SingleOrDefaultAsync(a => a.Id == id);
        }

        public Task<IEnumerable<Publisher>> GetPublishersAsync()
        {
            return Task.Run(() => context.Publishers.AsEnumerable());
        }
    }
}
