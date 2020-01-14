using Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModels.Services;

namespace ViewModels
{
    public class BookListViewModel : ViewModelBase
    {
        IBookService bookService;
        IFrameNavigationService navService;
        public BookListViewModel(IBookService bookService, IFrameNavigationService navService)
        {
            this.bookService = bookService;
            this.navService = navService;
            LoadBooksCommand = new RelayCommand(LoadBooks);
        }

        public string ErrorMsg { get; set; }

        public RelayCommand LoadBooksCommand { get; private set; }
        public ObservableCollection<Book> BookList { get; set; }

        public async void LoadBooks()
        {
            try
            {
                var list = await bookService.GetBooksAsync();
                BookList = new ObservableCollection<Book>(list);
                ErrorMsg = null;
                RaisePropertyChanged(nameof(BookList));
            }
            catch (Exception e)
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
