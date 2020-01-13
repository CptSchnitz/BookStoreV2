using DAL;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfiguration Configuration { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = configBuilder.Build();
            var connectionString = Configuration.GetConnectionString("SqlConnection");
            ConfigureServices();

            SimpleIoc.Default.Register<StoreContext>(() =>
            {
                var options = new DbContextOptionsBuilder<StoreContext>().UseSqlServer(connectionString,x => x.MigrationsAssembly("DAL")).Options;
                return new StoreContext(options);
            });

            base.OnStartup(e);
        }

        private void ConfigureServices()
        {

        }
    }
}
