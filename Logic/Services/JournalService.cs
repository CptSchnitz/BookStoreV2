using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class JournalService :ServiceBase ,IJournalService
    {
        IJournalRepository journalRepository;
        public JournalService(IJournalRepository journalRepository, ILogger logger) : base(logger)
        {
            this.journalRepository = journalRepository;
        }
    }
}
