using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using System.IO;
using ViewModels.Services;

namespace ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        IFrameNavigationService navigationService;
        public MainWindowViewModel(IFrameNavigationService navigationService)
        {
            this.navigationService = navigationService;

            BookListNavCommand = new RelayCommand(NavigateToBookList);
            JournalListNavCommand = new RelayCommand(NavigateToJournalList);
            BookEditNavCommand = new RelayCommand(NavigateToBookEdit);
            JournalEditNavCommand = new RelayCommand(NavigateToJournalEdit);
            AuthorNavCommand = new RelayCommand(NavigateToAuthor);
            PublisherNavCommand = new RelayCommand(NavigateToPublisher);
            GenreNavCommand = new RelayCommand(NavigateToGenre);
            HomeNavCommand = new RelayCommand(NavigateToHome);
            OpenLogFolderCommand = new RelayCommand(OpenLogFolder);
        }
        public RelayCommand OpenLogFolderCommand { get; private set; }
        public RelayCommand BookListNavCommand { get; private set; }
        public RelayCommand JournalListNavCommand { get; private set; }
        public RelayCommand BookEditNavCommand { get; private set; }
        public RelayCommand JournalEditNavCommand { get; private set; }
        public RelayCommand AuthorNavCommand { get; private set; }
        public RelayCommand PublisherNavCommand { get; private set; }
        public RelayCommand GenreNavCommand { get; private set; }
        public RelayCommand HomeNavCommand { get; private set; }

        private void NavigateToBookList() =>
            navigationService.NavigateTo("BookList");
        private void NavigateToJournalList() =>
            navigationService.NavigateTo("JournalList");
        private void NavigateToBookEdit() =>
            navigationService.NavigateTo("Book");
        private void NavigateToJournalEdit() =>
            navigationService.NavigateTo("Journal");
        private void NavigateToAuthor() =>
            navigationService.NavigateTo("Author");
        private void NavigateToPublisher() =>
            navigationService.NavigateTo("Publisher");
        private void NavigateToGenre() =>
            navigationService.NavigateTo("Genre");
        private void NavigateToHome() =>
            navigationService.NavigateTo("Home");
        private void OpenLogFolder()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            Process.Start("explorer.exe",currentDirectory + @"\Logs");
        }

    }
}
