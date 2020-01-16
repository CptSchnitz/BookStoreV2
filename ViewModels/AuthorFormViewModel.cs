using Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace ViewModels
{
    public class AuthorFormViewModel : ValidationViewModelBase
    {
        IAuthorService authorService;
        public AuthorFormViewModel(IAuthorService authorService)
        {
            this.authorService = authorService;
            AddAuthorCommand = new RelayCommand(AddAuthor);
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        [MaxLength(30, ErrorMessage = "The name is too long")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        [MaxLength(80, ErrorMessage = "The last name is too long")]
        public string LastName { get; set; }

        [MaxLength(80, ErrorMessage = "The pseudu name is too long")]
        public string PseuduName { get; set; }
        public string ErrorMsg { get; set; }

        public RelayCommand AddAuthorCommand { get; set; }

        public async void AddAuthor()
        {
            try
            {
                var author = new Author
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    PseuduName = PseuduName
                };
                var newAuthor = await authorService.AddAuthorAsync(author);
                Messenger.Default.Send<Author>(newAuthor);
                ErrorMsg = null;
                CleanForm();
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

        private void CleanForm()
        {
            FirstName = "";
            LastName = "";
            PseuduName = "";
            RaisePropertyChanged("FirstName");
            RaisePropertyChanged("LastName");
            RaisePropertyChanged("PseuduName");
        }
    }
}
