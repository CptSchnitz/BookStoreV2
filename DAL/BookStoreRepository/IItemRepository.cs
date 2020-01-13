using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.BookStoreRepository
{
    public interface IItemRepository
    {
        Task<IEnumerable<AbstractItem>> GetAbstractItemsAsync();
        Task<AbstractItem> GetAbstractItemByIdAsync(int id);
    }
}
