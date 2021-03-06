﻿using Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.API;
using System.Collections.ObjectModel;
using System.Data;

namespace ViewModels
{
    // view model to display publisher list
    public class PublisherListViewModel : ViewModelBase
    {
        IPublisherService publisherService;
        public PublisherListViewModel(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
            LoadPublishersCommand = new RelayCommand(LoadPublishers);
            Messenger.Default.Register<Publisher>(this, (publisher) => PublisherList.Add(publisher));
        }

        public RelayCommand LoadPublishersCommand { get; private set; }
        public ObservableCollection<Publisher> PublisherList { get; set; }

        public string ErrorMsg { get; set; }

        public async void LoadPublishers()
        {
            try
            {
                var list = await publisherService.GetPublishersAsync();
                PublisherList = new ObservableCollection<Publisher>(list);
                ErrorMsg = null;
                RaisePropertyChanged(nameof(PublisherList));
            }
            catch (DataException e)
            {
                ErrorMsg = e.Message;
            }
            finally
            {
                RaisePropertyChanged(nameof(ErrorMsg));
            }
        }
    }
}
