using Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Threading;

namespace ViewModels
{
    public class GenreFormViewModel : ValidationViewModelBase
    {
        IGenreService genreService;
        public GenreFormViewModel(IGenreService genreService)
        {
            this.genreService = genreService;
            AddGenreCommand = new RelayCommand(AddGenre);
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "The name is too long")]
        public string Name { get; set; }
        public string ErrorMsg { get; set; }

        public RelayCommand AddGenreCommand { get; set; }

        public async void AddGenre()
        {
            try
            {
                var genre = new Genre
                {
                    Name = Name
                };
                var newGenre = await genreService.AddGenreAsync(genre);
                Messenger.Default.Send<Genre>(newGenre);
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
            Name = null;
            RaisePropertyChanged("Name");
        }
    }
}
