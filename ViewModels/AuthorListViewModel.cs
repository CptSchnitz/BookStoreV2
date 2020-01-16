using Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace ViewModels
{
    public class AuthorListViewModel : ViewModelBase
    {
        IAuthorService authorService;
        public AuthorListViewModel(IAuthorService authorService)
        {
            this.authorService = authorService;
            LoadAuthorsCommand = new RelayCommand(LoadAuthors);
            Messenger.Default.Register<Author>(this, (author) => AuthorList.Add(author));
        }

        public RelayCommand LoadAuthorsCommand { get; private set; }
        public ObservableCollection<Author> AuthorList { get; set; }

        public string ErrorMsg { get; set; }

        public async void LoadAuthors()
        {
            try
            {
                var list = await authorService.GetAuthorsAsync();
                AuthorList = new ObservableCollection<Author>(list);
                ErrorMsg = null;
                RaisePropertyChanged(nameof(AuthorList));
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
