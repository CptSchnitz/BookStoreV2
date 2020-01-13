using CommonServiceLocator;
using DAL;
using GalaSoft.MvvmLight.Ioc;
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
        public IConfiguration Configuration { get; private set; }
        ILogger logger;
        protected override void OnStartup(StartupEventArgs e)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = configBuilder.Build();
            var connectionString = Configuration.GetConnectionString("SqlConnection");
            ConfigureLogger();
            ConfigureServices();
            SetupNavigation();

            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>()
                .UseSqlServer(connectionString, x => x.MigrationsAssembly("DAL"));
            var context =  new StoreContext(optionsBuilder.Options);
            SimpleIoc.Default.Register<StoreContext>(() => context);

            logger.Information("Done loading Services.");
            base.OnStartup(e);
        }

        private void ConfigureLogger()
        {
            logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            SimpleIoc.Default.Register<ILogger>(() => logger);
        }

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
            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }

        private void ConfigureServices()
        {
            SimpleIoc.Default.Register<IMessageBoxService, WpfMessageBoxService>();
        }
    }
}
