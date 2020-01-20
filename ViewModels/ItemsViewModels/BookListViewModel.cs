using Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Logic.API;
using System.Collections.ObjectModel;
using System.Data;
using ViewModels.Services;

namespace ViewModels.ItemsViewModels
{
    //  view model to show all the books
    public class BookListViewModel : ViewModelBase
    {
        IBookService bookService;
        IFrameNavigationService navService;
        private string errorMsg;

        public BookListViewModel(IBookService bookService, IFrameNavigationService navService)
        {
            this.bookService = bookService;
            this.navService = navService;
            LoadBooksCommand = new RelayCommand(LoadBooks);
            EditBookCommand = new RelayCommand<Book>(NavToEdit);
        }

        public string ErrorMsg { get => errorMsg; set => Set(ref errorMsg,value); }

        // command to edit books
        public RelayCommand<Book> EditBookCommand { get; private set; }
        //command to load books
        public RelayCommand LoadBooksCommand { get; private set; }
        public ObservableCollection<Book> BookList { get; set; }

        // go to the book edit page, and supplies the book to edit as a parameter
        private void NavToEdit(Book book)
        {
            navService.NavigateTo("Book", book);
        }
        public async void LoadBooks()
        {
            try
            {
                var list = await bookService.GetBooksAsync();
                BookList = new ObservableCollection<Book>(list);
                ErrorMsg = null;
                RaisePropertyChanged(nameof(BookList));
            }
            catch (DataException e)
            {
                ErrorMsg = e.Message;
            }
        }
    }
}
