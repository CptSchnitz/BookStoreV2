using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.API
{
    // service for journal operations
    public interface IJournalService
    {
        Task<List<Journal>> GetJournalsAsync();
        Task<Journal> AddJournalAsync(Journal journal);
        Task<Journal> UpdateJournalAsync(Journal journal);
    }
}
