using Common.Model;
using DAL.BookStoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicUt.Mocks
{
    class PublisherRepositoryMock : IPublisherRepository
    {
        private List<Publisher> publishers = new List<Publisher>()
        {
            new Publisher
            {
                Id = 1,
                Name = "a",
                ContactEmail="a@a.a"
            },
            new Publisher
            {
                Id = 2,
                Name = "b",
                ContactEmail="b@b.b"
            }
        };

        public Task<Publisher> CreatePublisherAsync(Publisher publisher)
        {
            publisher.Id = publishers.Last().Id + 1;
            publishers.Add(publisher);
            return Task.Run(() => publisher);
        }

        public Task<Publisher> EditPublisherAsync(Publisher publisher)
        {
            var index = publishers.FindIndex(a => a.Id == publisher.Id);
            publishers[index] = publisher;
            return Task.Run(() => publisher);
        }

        public Task<Publisher> GetPublisherByIdAsync(int id)
        {
            return Task.Run(() => publishers.FirstOrDefault(a => a.Id == id));
        }

        public Task<IEnumerable<Publisher>> GetPublishersAsync()
        {
            return Task.Run(() => publishers.AsEnumerable());
        }
    }
}
