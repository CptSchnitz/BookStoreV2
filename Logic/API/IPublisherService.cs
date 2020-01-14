using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.API
{
    public interface IPublisherService
    {
        Task<Publisher> AddPublisherAsync(Publisher publisher);

        Task<List<Publisher>> GetPublishersAsync();
    }
}
