using GalaSoft.MvvmLight.Ioc;

namespace ViewModels
{
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
    }
}
