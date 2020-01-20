using Common.Model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Logic.Search
{
    public class ItemSearch : IItemSearch
    {
        internal Dictionary<string, SearchFunc> dictionary;
        private string title;
        private decimal? minPrice;
        private decimal? maxPrice;
        private decimal? minDiscountedPrice;
        private decimal? maxDiscountedPrice;
        private string discountType;
        private DateTime? startPublishDate;
        private DateTime? endPublishDate;
        private int? minAmountInStock;
        private int? maxAmountInStock;
        private string publisherName;
        private string genre;

        internal delegate IEnumerable<AbstractItem> SearchFunc(IEnumerable<AbstractItem> items, ItemSearch param);


        public ItemSearch()
        {
            dictionary = new Dictionary<string, SearchFunc>();
        }

        public IEnumerable<AbstractItem> Search(IEnumerable<AbstractItem> items)
        {
            var funcs = dictionary.Values;
            foreach (var func in funcs)
            {
                items = func(items, this);
            }

            return items;
        }

        private void SetFunc<T>(ref T field, T value, SearchFunc func, [CallerMemberName] string propertyName = null)
        {
            if (value == null)
            {
                if (dictionary.ContainsKey(propertyName))
                    dictionary.Remove(propertyName);
            }
            else if (field == null)
            {
                dictionary[propertyName] = func;
            }
            field = value;
        }

        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = null;
                SetFunc(ref title, value, SearchFuncs.FilterByTitle);
            }
        }
        public decimal? MinPrice 
        { 
            get => minPrice; 
            set => SetFunc(ref minPrice,value,SearchFuncs.FilterByMinPrice); 
        }
        public decimal? MaxPrice 
        { 
            get => maxPrice;
            set => SetFunc(ref maxPrice, value, SearchFuncs.FilterByMaxPrice);
        }
        public decimal? MinDiscountedPrice 
        { 
            get => minDiscountedPrice;
            set => SetFunc(ref minDiscountedPrice, value, SearchFuncs.FilterByMinDiscount);
        }
        public decimal? MaxDiscountedPrice {
            get => maxDiscountedPrice;
            set => SetFunc(ref maxDiscountedPrice, value, SearchFuncs.FilterByMaxDiscount);
        }
        public string DiscountType {
            get => discountType;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = null;
                SetFunc(ref discountType, value, SearchFuncs.FilterByDiscountType);
            }
        }
        public DateTime? StartPublishDate 
        {
            get => startPublishDate;
            set => SetFunc(ref startPublishDate, value, SearchFuncs.FilterByStartDate);
        }
        public DateTime? EndPublishDate 
        {
            get => endPublishDate;
            set => SetFunc(ref endPublishDate, value, SearchFuncs.FilterByEndDate);
        }
        public int? MinAmountInStock 
        {
            get => minAmountInStock;
            set => SetFunc(ref minAmountInStock, value, SearchFuncs.FilterByMinAmount);
        }
        public int? MaxAmountInStock 
        {
            get => maxAmountInStock;
            set => SetFunc(ref maxAmountInStock, value, SearchFuncs.FilterByMaxAmount);
        }
        public string PublisherName 
        {
            get => publisherName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = null;
                SetFunc(ref publisherName, value, SearchFuncs.FilterByPublisherName);
            }
        }
        public string Genre 
        {
            get => genre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = null;
                SetFunc(ref genre, value, SearchFuncs.FilterByGenre);
            }
        }

    }
}
