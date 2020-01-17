using Common.Model;
using GalaSoft.MvvmLight.Command;
using Logic.API;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace ViewModels.DiscountViewModels
{
    // the view model for adding a new author discount
    public class DiscountAuthorFormViewModel : DiscountFormBaseViewModel
    {
        IAuthorService authorService;
        public DiscountAuthorFormViewModel(IDiscountService discountService, IAuthorService authorService) : base(discountService)
        {
            this.authorService = authorService;
            LoadAuthorsCommand = new RelayCommand(LoadAuthors);
        }
        // the command to load the authors
        public RelayCommand LoadAuthorsCommand { get; private set; }
        public ObservableCollection<Author> AuthorList { get; set; }
        public Author SelectedAuthor { get; set; }
        public async void LoadAuthors()
        {
            try
            {
                // loads the authors and notify the view that it changed
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

            //sets a selected author so its not null
            SelectedAuthor = AuthorList.FirstOrDefault();
            RaisePropertyChanged(nameof(SelectedAuthor));
        }

        public override void AddDiscount()
        {
            AddDiscount(new AuthorDiscount(SelectedAuthor, int.Parse(DiscountAmount)));
        }
    }
}
