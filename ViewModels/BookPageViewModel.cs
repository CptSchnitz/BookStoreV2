using Common.Model;
using Common.Validations;
using GalaSoft.MvvmLight.Command;
using Logic.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

using ViewModels.Services;

namespace ViewModels
{
    public class BookPageViewModel : ValidationViewModelBase
    {
        #region Services
        IAuthorService authorService;
        IPublisherService publisherService;
        IGenreService genreService;
        IBookService bookService;
        IFrameNavigationService navService;
        IMessageBoxService messageService;
        #endregion

        #region ModelFields
        private string title;
        private string price;
        private DateTime publishDate;
        private Publisher publisher;
        private string edition;
        private string isbn;
        private string description;
        private Author author;
        private ObservableCollection<Author> authorList;
        private ObservableCollection<Publisher> publisherList;
        private ObservableCollection<Genre> genreList;
        private string errorMsg;
        private List<Genre> selectedGenres;
        #endregion

        private Book bookToEdit;
        public BookPageViewModel(IAuthorService authorService, IPublisherService publisherService,
            IGenreService genreService, IBookService bookService, IFrameNavigationService navService, IMessageBoxService messageService)
        {
            this.authorService = authorService;
            this.publisherService = publisherService;
            this.genreService = genreService;
            this.bookService = bookService;
            this.navService = navService;
            this.messageService = messageService;

            LoadedCommand = new RelayCommand(OnLoaded);
            GenresChangedCommand = new RelayCommand<IList>(UpdateSelectedGenres);
            SubmitCommand = new RelayCommand(SubmitBook);
        }
        public string ErrorMsg { get => errorMsg; set => Set(ref errorMsg, value); }
        public ObservableCollection<Author> AuthorList { get => authorList; set => Set(ref authorList, value); }
        public ObservableCollection<Publisher> PublisherList { get => publisherList; set => Set(ref publisherList, value); }
        public ObservableCollection<Genre> GenreList { get => genreList; set => Set(ref genreList, value); }
        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand SubmitCommand { get; private set; }
        public RelayCommand<IList> GenresChangedCommand { get; private set; }

        #region ModelProps
        [Required]
        [MaxLength(100, ErrorMessage = "Title is too long")]
        public string Title { get => title; set => Set(ref title, value); }
        [Range(1.0, 1000.0)]
        [Required]
        public string Price { get => price; set => Set(ref price, value); }
        [Required]
        public DateTime PublishDate { get => publishDate; set => Set(ref publishDate, value); }
        public Publisher Publisher { get => publisher; set => Set(ref publisher, value); }
        [Range(1, 100)]
        [Required]
        public string Edition { get => edition; set => Set(ref edition, value); }

        [Required]
        public string Isbn { get => isbn; set => Set(ref isbn, value); }

        [MaxLength(200)]
        public string Description { get => description; set => Set(ref description, value); }
        public Author Author { get => author; set => Set(ref author, value); }

        [CollectionNotEmpty]
        public List<Genre> SelectedGenres { get => selectedGenres; set => Set(ref selectedGenres, value); }
        #endregion

        public async void OnLoaded()
        {
            try
            {
                AuthorList = new ObservableCollection<Author>(await authorService.GetAuthorsAsync());
                PublisherList = new ObservableCollection<Publisher>(await publisherService.GetPublishersAsync());
                GenreList = new ObservableCollection<Genre>(await genreService.GetGenresAsync());
                ErrorMsg = null;
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message;
            }
            
            bookToEdit = navService.Parameter as Book;
            if (bookToEdit != null)
            {
                SetFormForEdit();
            }
            else
            {
                ResetForm();
            }
        }

        private void SetFormForEdit()
        {
            Title = bookToEdit.Title;
            Price = bookToEdit.Price.ToString();
            Publisher = PublisherList.FirstOrDefault(pub => pub.Id == bookToEdit.PublisherId);
            Author = AuthorList.FirstOrDefault(author => author.Id == bookToEdit.AuthorId);
            Edition = bookToEdit.Edition.ToString();
            Description = bookToEdit.Description;
            PublishDate = bookToEdit.PublishDate;
            Isbn = bookToEdit.Isbn;
            List<int> genresId = bookToEdit.ItemGenres.Select(ig => ig.GenreId).ToList();
            SelectedGenres = genreList.Where(g => genresId.Contains(g.Id)).ToList();
            RaisePropertyChanged(nameof(IsModelValid));
        }

        private async void SubmitBook()
        {
            try
            {
                var book = new Book
                {
                    Title = Title,
                    AuthorId = author.Id,
                    Edition = int.Parse(edition),
                    Description = description,
                    Price = decimal.Parse(price),
                    Isbn = isbn,
                    PublisherId = publisher.Id,
                    PublishDate = publishDate,
                    ItemGenres = selectedGenres.Select(genre => new ItemGenre { GenreId = genre.Id }).ToList()
                };
                string msg;
                if (bookToEdit == null)
                {
                    await bookService.AddBookAsync(book);
                    msg = "Book Created :)";
                }
                else
                {
                    book.Id = bookToEdit.Id;
                    await bookService.UpdateBookAsync(book);
                    msg = "Book Edited :)";
                }
                ErrorMsg = null;
                messageService.ShowMessage("BookStore", msg);
                navService.NavigateTo("BookList");
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message;
            }
        }

        private void ResetForm()
        {
            Title = string.Empty;
            Price = string.Empty;
            Publisher = PublisherList.FirstOrDefault();
            Author = AuthorList.FirstOrDefault();
            Edition = string.Empty;
            Description = string.Empty;
            Isbn = string.Empty;
            SelectedGenres = null;
            RaisePropertyChanged(nameof(IsModelValid));
        }

        private void UpdateSelectedGenres(IList genreList)
        {
            SelectedGenres = genreList.Cast<Genre>().ToList();
            RaisePropertyChanged(nameof(IsModelValid));
        }
    }
}
