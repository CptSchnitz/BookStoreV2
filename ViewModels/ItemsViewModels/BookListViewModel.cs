using Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using ViewModels.Services;

namespace ViewModels.ItemsViewModels
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
            EditBookCommand = new RelayCommand<Book>(NavToEdit);
        }

        public string ErrorMsg { get; set; }

        public RelayCommand<Book> EditBookCommand { get; private set; }
        public RelayCommand LoadBooksCommand { get; private set; }
        public ObservableCollection<Book> BookList { get; set; }


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
            finally
            {
                RaisePropertyChanged(nameof(ErrorMsg));
            }
        }
    }
}
