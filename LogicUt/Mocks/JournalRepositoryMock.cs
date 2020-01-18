using Common.Model;
using DAL.BookStoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicUt.Mocks
{
    class JournalRepositoryMock : IJournalRepository
    {
        private List<Journal> journals = new List<Journal>()
        {
            new Journal
            {
                Id = 1,
                AmountInStock = 5,
                Issn = "a",
                IssueNum = 1,
                ItemGenres = new List<ItemGenre>()
                {
                    new ItemGenre
                    {
                        AbstractItemId=1,
                        Genre = new Genre {Id = 1, Name="test"},
                        GenreId = 1
                    }
                },
                Price = 100m,
                PublishDate = new DateTime(2000,1,1),
                Title = "A",
                Publisher = new Publisher{Id = 1, Name ="a",ContactEmail="a@a.com"},
                PublisherId = 1
            },
        };

        public Task<Journal> CreateJournalAsync(Journal journal)
        {
            journal.Id = journals.Last().Id + 1;
            journals.Add(journal);
            return Task.Run(() => journal);
        }

        public Task<Journal> EditJournalAsync(Journal journal)
        {
            var index = journals.FindIndex(a => a.Id == journal.Id);
            journals[index] = journal;
            return Task.Run(() => journal);
        }

        public Task<Journal> GetJournalByIdAsync(int id)
        {
            return Task.Run(() => journals.FirstOrDefault(a => a.Id == id));
        }

        public Task<IEnumerable<Journal>> GetJournalsAsync()
        {
            return Task.Run(() => journals.AsEnumerable());
        }
    }
}
