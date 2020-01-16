using Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using ViewModels.Services;

namespace ViewModels.ItemsViewModels
{
    public class JournalListViewModel : ViewModelBase
    {
        IJournalService journalService;
        IFrameNavigationService navService;
        public JournalListViewModel(IJournalService journalService, IFrameNavigationService navService)
        {
            this.journalService = journalService;
            this.navService = navService;
            LoadJournalsCommand = new RelayCommand(LoadJournals);
            EditJournalCommand = new RelayCommand<Journal>(NavToEdit);
        }

        public string ErrorMsg { get; set; }

        public RelayCommand<Journal> EditJournalCommand { get; private set; }
        public RelayCommand LoadJournalsCommand { get; private set; }
        public ObservableCollection<Journal> JournalList { get; set; }


        private void NavToEdit(Journal journal)
        {
            navService.NavigateTo("Journal", journal);
        }
        public async void LoadJournals()
        {
            try
            {
                var list = await journalService.GetJournalsAsync();
                JournalList = new ObservableCollection<Journal>(list);
                ErrorMsg = null;
                RaisePropertyChanged(nameof(JournalList));
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
