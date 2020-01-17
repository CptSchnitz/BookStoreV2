using DAL;
using DAL.BookStoreRepository;
using DAL.EfBookStoreRepository;
using GalaSoft.MvvmLight.Ioc;
using Logic.API;
using Logic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Windows;
using ViewModels.Services;
using WPF.Common.Services;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string configPath = "appsettings.json";

        public IConfiguration Configuration { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            // Loading the configuration from file
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configPath, optional: false, reloadOnChange: true);
            Configuration = configBuilder.Build();

            ConfigureServices();
            SetupNavigation();

            base.OnStartup(e);
        }

        // Configures and registers a logger based on the configuration
        private void ConfigureLogger()
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            SimpleIoc.Default.Register<ILogger>(() => logger);
        }

        // adds all the pages to the navigation service and registers it
        private void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("Author", new Uri("./Views/AuthorPage.xaml", UriKind.Relative));
            navigationService.Configure("BookList", new Uri("./Views/BookListPage.xaml", UriKind.Relative));
            navigationService.Configure("Book", new Uri("./Views/BookEditPage.xaml", UriKind.Relative));
            navigationService.Configure("JournalList", new Uri("./Views/JournalListPage.xaml", UriKind.Relative));
            navigationService.Configure("Journal", new Uri("./Views/JournalEditPage.xaml", UriKind.Relative));
            navigationService.Configure("Publisher", new Uri("./Views/PublisherPage.xaml", UriKind.Relative));
            navigationService.Configure("Genre", new Uri("./Views/GenrePage.xaml", UriKind.Relative));
            navigationService.Configure("Home", new Uri("./Views/HomePage.xaml", UriKind.Relative));
            navigationService.Configure("Discount", new Uri("./Views/DiscountPage.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }

        // Registers all the services required by the app
        private void ConfigureServices()
        {
            SetDbContext();
            ConfigureLogger();

            SimpleIoc.Default.Register<IMessageBoxService, WpfMessageBoxService>();

            SimpleIoc.Default.Register<IGenreService, GenreService>();
            SimpleIoc.Default.Register<IGenreRepository, EfGenreRepository>();

            SimpleIoc.Default.Register<IPublisherService, PublisherService>();
            SimpleIoc.Default.Register<IPublisherRepository, EfPublisherRepository>();

            SimpleIoc.Default.Register<IAuthorService, AuthorService>();
            SimpleIoc.Default.Register<IAuthorRepository, EfAuthorRepository>();

            SimpleIoc.Default.Register<IItemRepository, EfItemRepository>();

            SimpleIoc.Default.Register<IJournalService, JournalService>();
            SimpleIoc.Default.Register<IJournalRepository, EfJournalRepository>();

            SimpleIoc.Default.Register<IBookService, BookService>();
            SimpleIoc.Default.Register<IBookRepository, EfBookRepository>();

            SimpleIoc.Default.Register<IDiscountService, DiscountService>();
            SimpleIoc.Default.Register<IDiscountRepository, EfDiscountRepository>();

        }

        // configures the dbcontext with the connection string from config
        private void SetDbContext()
        {
            var connectionString = Configuration.GetConnectionString("SqlConnection");
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>()
                .UseSqlServer(connectionString, x => x.MigrationsAssembly("DAL"))
                .UseLazyLoadingProxies();
            SimpleIoc.Default.Register<StoreContextFactory>(() => new StoreContextFactory(optionsBuilder.Options));
        }
    }
}
