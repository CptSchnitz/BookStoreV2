using GalaSoft.MvvmLight.Ioc;
using ViewModels.DiscountViewModels;
using ViewModels.ItemsViewModels;

namespace ViewModels
{
    // locators that registers all the view models and exposes them for use in views
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainWindowViewModel>();

            SimpleIoc.Default.Register<GenreListViewModel>();
            SimpleIoc.Default.Register<GenreFormViewModel>();

            SimpleIoc.Default.Register<AuthorListViewModel>();
            SimpleIoc.Default.Register<AuthorFormViewModel>();

            SimpleIoc.Default.Register<PublisherListViewModel>();
            SimpleIoc.Default.Register<PublisherFormViewModel>();

            SimpleIoc.Default.Register<BookPageViewModel>();
            SimpleIoc.Default.Register<BookListViewModel>();

            SimpleIoc.Default.Register<JournalPageViewModel>();
            SimpleIoc.Default.Register<JournalListViewModel>();

            SimpleIoc.Default.Register<DiscountListViewModel>();
            SimpleIoc.Default.Register<DiscountAuthorFormViewModel>();
            SimpleIoc.Default.Register<DiscountGenreFormViewModel>();
            SimpleIoc.Default.Register<DiscountDateFormViewModel>();
            SimpleIoc.Default.Register<DiscountPublisherFormViewModel>();

            SimpleIoc.Default.Register<SearchFormViewModel>();
            SimpleIoc.Default.Register<SearchListViewModel>();
        }

        public MainWindowViewModel Main => SimpleIoc.Default.GetInstance<MainWindowViewModel>();
        public GenreListViewModel GenreList => SimpleIoc.Default.GetInstance<GenreListViewModel>();
        public GenreFormViewModel GenreForm => SimpleIoc.Default.GetInstance<GenreFormViewModel>();
        public AuthorListViewModel AuthorList => SimpleIoc.Default.GetInstance<AuthorListViewModel>();
        public AuthorFormViewModel AuthorForm => SimpleIoc.Default.GetInstance<AuthorFormViewModel>();
        public PublisherListViewModel PublisherList => SimpleIoc.Default.GetInstance<PublisherListViewModel>();
        public PublisherFormViewModel PublisherForm => SimpleIoc.Default.GetInstance<PublisherFormViewModel>();
        public BookPageViewModel BookPage => SimpleIoc.Default.GetInstance<BookPageViewModel>();
        public BookListViewModel BookList => SimpleIoc.Default.GetInstance<BookListViewModel>();
        public JournalPageViewModel JournalPage => SimpleIoc.Default.GetInstance<JournalPageViewModel>();
        public JournalListViewModel JournalList => SimpleIoc.Default.GetInstance<JournalListViewModel>();
        public DiscountListViewModel DiscountList => SimpleIoc.Default.GetInstance<DiscountListViewModel>();
        public DiscountAuthorFormViewModel DiscountAuthor => SimpleIoc.Default.GetInstance<DiscountAuthorFormViewModel>();
        public DiscountGenreFormViewModel DiscountGenre => SimpleIoc.Default.GetInstance<DiscountGenreFormViewModel>();
        public DiscountDateFormViewModel DiscountDate => SimpleIoc.Default.GetInstance<DiscountDateFormViewModel>();
        public DiscountPublisherFormViewModel DiscountPublisher => SimpleIoc.Default.GetInstance<DiscountPublisherFormViewModel>();

        public SearchFormViewModel SearchForm => SimpleIoc.Default.GetInstance<SearchFormViewModel>();
        public SearchListViewModel SearchList => SimpleIoc.Default.GetInstance<SearchListViewModel>();
    }
}
