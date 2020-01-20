using Common.Model;
using System.Collections.Generic;

namespace Logic.Search
{
    public interface IItemSearch
    {
        IEnumerable<AbstractItem> Search(IEnumerable<AbstractItem> items);
    }
}