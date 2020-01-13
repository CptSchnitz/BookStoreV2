using Common.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.BookStoreRepository
{
    public interface IJournalRepository
    {
        Task<IEnumerable<Journal>> GetJournalsAsync();
        Task<Journal> GetJournalByIdAsync(int id);
        Task<Journal> CreateJournalAsync(Journal journal);
        Task<Journal> EditJournalAsync(Journal journal);
    }
}
