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
using System.Threading.Tasks;
using ViewModels.Services;

namespace ViewModels.ItemsViewModels
{
    // view model for adding a new book
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
        private DateTime publishDate = DateTime.Now;
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
        private string amountInStock;
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

        // all the properties for the model. when value is changed, the UI is notified.
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
        [Range(0, 1000)]
        public string AmountInStock { get => amountInStock; set => Set( ref amountInStock, value); }
        [Required]
        [MaxLength(20)]
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
                // load authors, publishers and genres to populate the form.
                var authorTask = authorService.GetAuthorsAsync();
                var publisherTask = publisherService.GetPublishersAsync();
                var genreTask = genreService.GetGenresAsync();

                // wait until all the loading is complete.
                await Task.WhenAll(authorTask, publisherTask, genreTask);

                // populates the lists.
                AuthorList = new ObservableCollection<Author>(await authorTask);
                PublisherList = new ObservableCollection<Publisher>(await publisherTask);
                GenreList = new ObservableCollection<Genre>(await genreTask);

                ErrorMsg = null;
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message;
            }

            // if creating new book, reset all the fields, otherwise sets the values to match the parameter
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

        // copies the value from the book to edit to the model properties.
        private void SetFormForEdit()
        {
            Title = bookToEdit.Title;
            Price = bookToEdit.Price.ToString();
            Publisher = PublisherList.FirstOrDefault(pub => pub.Id == bookToEdit.PublisherId);
            Author = AuthorList.FirstOrDefault(author => author.Id == bookToEdit.AuthorId);
            Edition = bookToEdit.Edition.ToString();
            Description = bookToEdit.Description;
            PublishDate = bookToEdit.PublishDate;
            AmountInStock = bookToEdit.AmountInStock.ToString();
            Isbn = bookToEdit.Isbn;
            List<int> genresId = bookToEdit.ItemGenres.Select(ig => ig.GenreId).ToList();
            SelectedGenres = genreList.Where(g => genresId.Contains(g.Id)).ToList();
            RaisePropertyChanged(nameof(IsModelValid));
        }

        // saves a new book to the db
        private async void SubmitBook()
        {
            try
            {
                //create a new book.
                var book = new Book
                {
                    Title = Title,
                    AuthorId = author.Id,
                    Edition = int.Parse(edition),
                    Description = description,
                    Price = decimal.Parse(price),
                    AmountInStock = int.Parse(amountInStock),
                    Isbn = isbn,
                    PublisherId = publisher.Id,
                    PublishDate = publishDate,
                    ItemGenres = selectedGenres.Select(genre => new ItemGenre { GenreId = genre.Id }).ToList()
                };

                string msg;

                //calls the correct service operation based if edit or new
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

                // navigate back to the book list page
                navService.NavigateTo("BookList");
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message;
            }
        }

        // sets all the model propeties to null
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
            AmountInStock = string.Empty;
            RaisePropertyChanged(nameof(IsModelValid));
        }

        private void UpdateSelectedGenres(IList genreList)
        {
            //this cast is required as the selectedItems property from wpf is Non generic IList
            SelectedGenres = genreList.Cast<Genre>().ToList();
            RaisePropertyChanged(nameof(IsModelValid));
        }
    }
}
