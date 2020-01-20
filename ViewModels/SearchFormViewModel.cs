using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.Search;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class SearchFormViewModel : ViewModelBase
    {
        private ItemSearch search = new ItemSearch();

        public SearchFormViewModel()
        {
            SubmitSearchCommand = new RelayCommand(SubmitSearch);
        }

        public RelayCommand SubmitSearchCommand { get; set; }
        public ItemSearch Search { get => search; set => Set (ref search,value); }

        private void SubmitSearch()
        {
            Messenger.Default.Send<IItemSearch>(Search);
        }
    }
}
