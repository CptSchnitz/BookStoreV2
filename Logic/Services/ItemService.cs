using DAL.BookStoreRepository;
using Logic.API;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Services
{
    public class ItemService : ServiceBase,IItemService
    {
        IItemRepository itemRepository;
        public ItemService(IItemRepository itemRepository, ILogger logger) : base(logger) 
        {
            this.itemRepository = itemRepository;
        }
    }
}
