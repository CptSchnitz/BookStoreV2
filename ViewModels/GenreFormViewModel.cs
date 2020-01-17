using Common.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Logic.API;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ViewModels
{
    // View Model for creating genres
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

                // sends a message with the new genre
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

        //reset the form
        private void CleanForm()
        {
            Name = null;
            RaisePropertyChanged("Name");
        }
    }
}
