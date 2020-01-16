using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using Common.Model;
using GalaSoft.MvvmLight.Command;
using Logic.API;

namespace ViewModels.DiscountViewModels
{
    public class DiscountGenreFormViewModel : DiscountFormBaseViewModel
    {
        IGenreService genreService;
        public DiscountGenreFormViewModel(IDiscountService discountService, IGenreService genreService) : base(discountService)
        {
            this.genreService = genreService;
            LoadGenresCommand = new RelayCommand(LoadGenres);
        }

        public RelayCommand LoadGenresCommand { get; private set; }
        public ObservableCollection<Genre> GenreList { get; set; }
        public Genre SelectedGenre { get; set; }
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
            SelectedGenre = GenreList.FirstOrDefault();
            RaisePropertyChanged(nameof(SelectedGenre));
        }

        public override void AddDiscount()
        {
            AddDiscount(new GenreDiscount(SelectedGenre, int.Parse(DiscountAmount)));
        }
    }
}
