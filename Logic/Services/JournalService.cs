using DAL.BookStoreRepository;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class JournalService : IJournalService
    {
        IJournalRepository journalRepository;
        public JournalService(IJournalRepository journalRepository)
        {
            this.journalRepository = journalRepository;
        }
    }
}
