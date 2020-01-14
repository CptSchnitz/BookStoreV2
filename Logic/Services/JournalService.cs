using Common.Model;
using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class JournalService : ServiceBase, IJournalService
    {
        IJournalRepository journalRepository;
        public JournalService(IJournalRepository journalRepository, ILogger logger) : base(logger)
        {
            this.journalRepository = journalRepository;
        }

        public Task<Journal> AddJournalAsync(Journal journal)
        {
            throw new NotImplementedException();
        }

        public Task<List<Journal>> GetJournalsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Journal> UpdateJournalAsync(Journal journal)
        {
            throw new NotImplementedException();
        }
    }
}
