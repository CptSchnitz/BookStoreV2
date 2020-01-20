using Common.Model;
using Logic.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.API
{
    public interface ISearchService
    {
        Task<List<AbstractItem>> GetSearchResults(IItemSearch search);
    }
}
