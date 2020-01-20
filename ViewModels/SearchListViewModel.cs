using GalaSoft.MvvmLight;
using Logic.API;
using Logic.Search;
using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using Common.Model;
using System.Collections.ObjectModel;
using System.Data;

namespace ViewModels
{
    public class SearchListViewModel : ViewModelBase
    {
        ISearchService searchService;
        private ObservableCollection<AbstractItem> itemsList;
        private string errorMsg;

        public SearchListViewModel(ISearchService service)
        {
            this.searchService = service;
            Messenger.Default.Register<IItemSearch>(this, ExecuteSearch);
        }

        public ObservableCollection<AbstractItem> ItemsList { get => itemsList; set => Set(ref itemsList, value); }

        public string ErrorMsg { get => errorMsg; set =>Set(ref errorMsg, value); }

        private async void ExecuteSearch(IItemSearch search)
        {
            try
            {
                var list = await searchService.GetSearchResults(search);
                ItemsList = new ObservableCollection<AbstractItem>(list);
                ErrorMsg = null;
            }
            catch (DataException e)
            {
                ErrorMsg = e.Message;
            }
        }
    }
}
