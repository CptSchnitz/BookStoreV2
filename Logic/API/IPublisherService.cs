using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.API
{
    // service for publisher operations
    public interface IPublisherService
    {
        Task<Publisher> AddPublisherAsync(Publisher publisher);

        Task<List<Publisher>> GetPublishersAsync();
    }
}
