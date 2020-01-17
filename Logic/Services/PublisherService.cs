using Common.Model;
using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class PublisherService : ServiceBase, IPublisherService
    {
        IPublisherRepository publisherRepository;
        public PublisherService(IPublisherRepository publisherRepository, ILogger logger) : base(logger)
        {
            this.publisherRepository = publisherRepository;
        }

        public async Task<Publisher> AddPublisherAsync(Publisher publisher)
        {
            try
            {
                var newPublisher = await publisherRepository.CreatePublisherAsync(publisher);
                return newPublisher;
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error Adding new Publisher");
                throw new DataException("Error Adding publisher to db");
            }
        }

        public async Task<List<Publisher>> GetPublishersAsync()
        {
            try
            {
                var publisherList = (await publisherRepository.GetPublishersAsync()).ToList();
                return publisherList;
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error loading Publishers");
                throw new DataException("Error getting data from db");
            }
        }
    }
}
