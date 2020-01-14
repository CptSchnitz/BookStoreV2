using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.API
{
    public interface IJournalService
    {
        Task<List<Journal>> GetJournalsAsync();
        Task<Journal> AddJournalAsync(Journal journal);
        Task<Journal> UpdateJournalAsync(Journal journal);
    }
}
