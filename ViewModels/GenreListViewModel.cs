using Common.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace ViewModels
{
    public class GenreListViewModel : ViewModelBase
    {
        IGenreService genreService;
        public GenreListViewModel(IGenreService genreService)
        {
            this.genreService = genreService;
            LoadGenresCommand = new RelayCommand(LoadGenres);
            Messenger.Default.Register<Genre>(this, (genre) => GenreList.Add(genre));
        }

        public RelayCommand LoadGenresCommand { get; private set; }
        public ObservableCollection<Genre> GenreList { get; set; }

        public string ErrorMsg { get; set; }

        public async void LoadGenres()
        {
            try
            {
                var list = await genreService.GetGenresAsync();
                GenreList = new ObservableCollection<Genre>(list);
                ErrorMsg = null;
                RaisePropertyChanged(nameof(GenreList));
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
