using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.BookStoreRepository
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetPublishersAsync();
        Task<Publisher> GetPublisherByIdAsync(int id);
        Task<Publisher> CreatePublisherAsync(Publisher publisher);
        Task<Publisher> EditPublisherAsync(Publisher publisher);
    }
}
