using Common.Model;
using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class PublisherService : ServiceBase ,IPublisherService
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
            catch (Exception e)
            {
                logger.Error(e, "Error Adding new Publisher");
                throw new Exception("Error Adding publisher to db");
            }
        }

        public async Task<List<Publisher>> GetPublishersAsync()
        {
            try
            {
                var publisherList = (await publisherRepository.GetPublishersAsync()).ToList();
                return publisherList;
            }
            catch (Exception e)
            {
                logger.Error(e, "Error loading Publishers");
                throw new Exception("Error getting data from db");
            }
        }
    }
}
