using Common.Model;
using DAL.BookStoreRepository;
using Logic.API;
using Logic.Search;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class SearchService : ServiceBase, ISearchService
    {
        IItemRepository itemRepository;
        IDiscountService discountService;

        public SearchService(IItemRepository itemRepository,IDiscountService discountService, ILogger logger) : base(logger)
        {
            this.itemRepository = itemRepository;
            this.discountService = discountService;
        }

        public async Task<List<AbstractItem>> GetSearchResults(IItemSearch search)
        {
            try
            {
                var items = await itemRepository.GetAbstractItemsAsync();
                await discountService.SetItemsPricesAsync(items);

                return search.Search(items).ToList();

            }
            catch (DataException e)
            {
                logger?.Error(e, "Error Adding new book");
                throw new DataException("Error Adding book to db");
            }


        }
    }
}
