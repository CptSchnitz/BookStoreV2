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
    public class JournalPageViewModel : ValidationViewModelBase
    {
        #region Services
        IPublisherService publisherService;
        IGenreService genreService;
        IJournalService journalService;
        IFrameNavigationService navService;
        IMessageBoxService messageService;
        #endregion

        #region ModelFields
        private string title;
        private string price;
        private DateTime publishDate = DateTime.Now;
        private Publisher publisher;
        private string issn;
        private string issueNum;
        private ObservableCollection<Publisher> publisherList;
        private ObservableCollection<Genre> genreList;
        private string errorMsg;
        private List<Genre> selectedGenres;
        #endregion

        private Journal journalToEdit;
        public JournalPageViewModel(IJournalService journalService, IPublisherService publisherService,
            IGenreService genreService, IFrameNavigationService navService, IMessageBoxService messageService)
        {
            this.publisherService = publisherService;
            this.genreService = genreService;
            this.journalService = journalService;
            this.navService = navService;
            this.messageService = messageService;

            LoadedCommand = new RelayCommand(OnLoaded);
            GenresChangedCommand = new RelayCommand<IList>(UpdateSelectedGenres);
            SubmitCommand = new RelayCommand(SubmitJournal);
        }
        public string ErrorMsg { get => errorMsg; set => Set(ref errorMsg, value); }
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

        [Required]
        public string Issn { get => issn; set => Set(ref issn, value); }

        [Required]
        [Range(1, 100)]
        public string IssueNum { get => issueNum; set => Set(ref issueNum, value); }

        [CollectionNotEmpty]
        public List<Genre> SelectedGenres { get => selectedGenres; set => Set(ref selectedGenres, value); }
        #endregion

        public async void OnLoaded()
        {
            try
            {
                // load all the publishers and genres to create a new book
                var publisherTask = publisherService.GetPublishersAsync();
                var genreTask = genreService.GetGenresAsync();

                await Task.WhenAll(publisherTask, genreTask);

                PublisherList = new ObservableCollection<Publisher>(await publisherTask);
                GenreList = new ObservableCollection<Genre>(await genreTask);

                ErrorMsg = null;
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message;
            }

            // set the model properties based if new journal or edit
            journalToEdit = navService.Parameter as Journal;
            if (journalToEdit != null)
            {
                SetFormForEdit();
            }
            else
            {
                ResetForm();
            }
        }

        // populates the model from the journalToEdit received as nav parameter
        private void SetFormForEdit()
        {
            Title = journalToEdit.Title;
            Price = journalToEdit.Price.ToString();
            Publisher = PublisherList.FirstOrDefault(pub => pub.Id == journalToEdit.PublisherId);
            PublishDate = journalToEdit.PublishDate;
            IssueNum = journalToEdit.IssueNum.ToString();
            Issn = journalToEdit.Issn;
            List<int> genresId = journalToEdit.ItemGenres.Select(ig => ig.GenreId).ToList();
            SelectedGenres = genreList.Where(g => genresId.Contains(g.Id)).ToList();
            RaisePropertyChanged(nameof(IsModelValid));
        }

        private async void SubmitJournal()
        {
            try
            {
                //create new journal
                var journal = new Journal
                {
                    Title = Title,
                    Price = decimal.Parse(price),
                    Issn = issn,
                    IssueNum = int.Parse(issueNum),
                    PublisherId = publisher.Id,
                    PublishDate = publishDate,
                    ItemGenres = selectedGenres.Select(genre => new ItemGenre { GenreId = genre.Id }).ToList()
                };
                string msg;

                // calls the correct journal service command based if edit or new journal
                if (journalToEdit == null)
                {
                    await journalService.AddJournalAsync(journal);
                    msg = "Journal Created :)";
                }
                else
                {
                    journal.Id = journalToEdit.Id;
                    await journalService.UpdateJournalAsync(journal);
                    msg = "Journal Edited :)";
                }
                ErrorMsg = null;

                // shows a message to the user, and goes to the journal list page
                messageService.ShowMessage("BookStore", msg);
                navService.NavigateTo("JournalList");
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message;
            }
        }

        // sets all the model properties to empty.
        private void ResetForm()
        {
            Title = string.Empty;
            Price = string.Empty;
            Publisher = PublisherList.FirstOrDefault();
            IssueNum = string.Empty;
            Issn = string.Empty;
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

