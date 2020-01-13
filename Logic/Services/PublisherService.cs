using DAL.BookStoreRepository;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class PublisherService : IPublisherService
    {
        IPublisherRepository publisherRepository;
        public PublisherService(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }
    }
}
