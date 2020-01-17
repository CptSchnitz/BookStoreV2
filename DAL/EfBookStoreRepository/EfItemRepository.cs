using Common.Model;
using DAL.BookStoreRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EfBookStoreRepository
{
    public class EfItemRepository : IItemRepository
    {
        private readonly StoreContext context;
        public EfItemRepository(StoreContextFactory factory)
        {
            this.context = factory.GetContext();
        }
        public async Task<AbstractItem> GetAbstractItemByIdAsync(int id)
        {
            return await context.Items
                .Include(i => i.ItemGenres)
                .ThenInclude(ig => ig.Genre)
                .SingleOrDefaultAsync(i => i.Id == id);
        }

        public Task<IEnumerable<AbstractItem>> GetAbstractItemsAsync()
        {
            return Task.Run(() => context.Items.Include(i => i.Publisher)
                .Include(i => i.ItemGenres)
                .ThenInclude(ig => ig.Genre).AsEnumerable());
        }
    }
}
