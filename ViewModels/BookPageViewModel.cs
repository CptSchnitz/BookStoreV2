using Common.Model;
using GalaSoft.MvvmLight.Command;
using Logic.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ViewModels.Services;

namespace ViewModels
{
    public class BookPageViewModel : ValidationViewModelBase
    {
        IAuthorService authorService;
        IPublisherService publisherService;
        IGenreService genreService;
        IBookService bookService;
        IFrameNavigationService navService;
        public BookPageViewModel(IAuthorService authorService,
                IPublisherService publisherService,
                IGenreService genreService, IBookService bookService,
                IFrameNavigationService navService)
        {
            this.authorService = authorService;
            this.publisherService = publisherService;
            this.genreService = genreService;
            this.bookService = bookService;
            this.navService = navService;
            LoadedCommand = new RelayCommand(OnLoaded);
        }
        public string ErrorMsg { get; set; }
        public ObservableCollection<Author> AuthorList { get; set; }
        public ObservableCollection<Publisher> PublisherList { get; set; }
        public ObservableCollection<Genre> GenreList { get; set; }
        public RelayCommand LoadedCommand { get; private set; }

        [Required]
        [MaxLength(100,ErrorMessage ="Title is too long")]
        public string Title { get; set; }
        [Range(1.0,1000.0)]
        [Required]
        public string Price { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        public Publisher Publisher { get; set; }
        [Range(1,100)]
        [Required]
        public string Edition { get; set; }
        [Required]
        public string Isbn { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public Author Author { get; set; }
        public ObservableCollection<Genre> SelectedGenres { get; set; } = new ObservableCollection<Genre>();

        public async void OnLoaded()
        {
            try
            { 
                AuthorList = new ObservableCollection<Author>(await authorService.GetAuthorsAsync());
                RaisePropertyChanged(nameof(AuthorList));
                PublisherList = new ObservableCollection<Publisher>(await publisherService.GetPublishersAsync());
                RaisePropertyChanged(nameof(PublisherList));
                GenreList = new ObservableCollection<Genre>(await genreService.GetGenresAsync());
                RaisePropertyChanged(nameof(GenreList));
                ErrorMsg = null;
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
