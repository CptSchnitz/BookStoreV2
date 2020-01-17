using Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.API;
using System.Collections.ObjectModel;
using System.Data;
using ViewModels.Services;

namespace ViewModels.ItemsViewModels
{
    // view model to display the list of journals
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

        //navigate to the journal edit page upon edit
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
