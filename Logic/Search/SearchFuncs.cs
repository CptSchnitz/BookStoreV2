using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Search
{
    static class SearchFuncs
    {
        internal static IEnumerable<AbstractItem> FilterByTitle(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.Title.StartsWith(param.Title));
        }

        internal static IEnumerable<AbstractItem> FilterByMinPrice(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.Price > param.MinPrice);
        }

        internal static IEnumerable<AbstractItem> FilterByMaxPrice(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.Price < param.MaxPrice);
        }

        internal static IEnumerable<AbstractItem> FilterByMinDiscount(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.DiscountedPrice > param.MinDiscountedPrice);
        }

        internal static IEnumerable<AbstractItem> FilterByMaxDiscount(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.DiscountedPrice < param.MaxDiscountedPrice);
        }

        internal static IEnumerable<AbstractItem> FilterByDiscountType(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.DiscountType == param.DiscountType);
        }

        internal static IEnumerable<AbstractItem> FilterByStartDate(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.PublishDate > param.StartPublishDate);
        }

        internal static IEnumerable<AbstractItem> FilterByEndDate(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.PublishDate < param.EndPublishDate);
        }

        internal static IEnumerable<AbstractItem> FilterByMinAmount(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.AmountInStock < param.MinDiscountedPrice);
        }

        internal static IEnumerable<AbstractItem> FilterByMaxAmount(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.AmountInStock > param.MaxDiscountedPrice);
        }

        internal static IEnumerable<AbstractItem> FilterByPublisherName(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.Publisher.Name.StartsWith(param.PublisherName));
        }

        internal static IEnumerable<AbstractItem> FilterByGenre(IEnumerable<AbstractItem> items, ItemSearch param)
        {
            return items.Where(i => i.ItemGenres.Any(ig => ig.Genre.Name == param.Genre));
        }
    }
}
