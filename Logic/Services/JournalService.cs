using Common.Model;
using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class JournalService : ServiceBase, IJournalService
    {
        IJournalRepository journalRepository;
        IDiscountService discountService;
        public JournalService(IJournalRepository journalRepository, IDiscountService discountService , ILogger logger) : base(logger)
        {
            this.discountService = discountService;
            this.journalRepository = journalRepository;
        }

        public async Task<Journal> AddJournalAsync(Journal journal)
        {
            try
            {
                var newJournal = await journalRepository.CreateJournalAsync(journal);
                return newJournal;
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error Adding new journal");
                throw new DataException("Error Adding journal to db");
            }
        }

        public async Task<List<Journal>> GetJournalsAsync()
        {
            List<Journal> journalList;
            try
            {
                journalList = (await journalRepository.GetJournalsAsync()).ToList();
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error getting journals");
                throw new DataException("Error getting journals from db");
            }
            await discountService.SetItemsPricesAsync(journalList.Cast<AbstractItem>());
            return journalList.ToList();
        }

        public async Task<Journal> UpdateJournalAsync(Journal journal)
        {
            try
            {
                var newJournal = await journalRepository.EditJournalAsync(journal);
                return newJournal;
            }
            catch (DataException e)
            {
                logger?.Error(e, "Error Journal book");
                throw new DataException("Error Journal book");
            }
        }
    }
}
