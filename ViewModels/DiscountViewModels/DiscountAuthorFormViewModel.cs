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
    public class DiscountAuthorFormViewModel : DiscountFormBaseViewModel
    {
        IAuthorService authorService;
        public DiscountAuthorFormViewModel(IDiscountService discountService, IAuthorService authorService) : base(discountService)
        {
            this.authorService = authorService;
            LoadAuthorsCommand = new RelayCommand(LoadAuthors);
        }

        public RelayCommand LoadAuthorsCommand { get; private set; }
        public ObservableCollection<Author> AuthorList { get; set; }
        public Author SelectedAuthor { get; set; }
        public async void LoadAuthors()
        {
            try
            {
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
            SelectedAuthor = AuthorList.FirstOrDefault();
            RaisePropertyChanged(nameof(SelectedAuthor));
        }

        public override void AddDiscount()
        {
            AddDiscount(new AuthorDiscount(SelectedAuthor, int.Parse(DiscountAmount)));
        }
    }
}
