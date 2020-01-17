using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WPF.EfMigration
{
    // this class is used by ef migrations to configure the db 
    class BookStoreFactory : IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configBuilder.Build();
            var connectionString = configuration.GetConnectionString("SqlConnection");

            var options = new DbContextOptionsBuilder<StoreContext>().UseSqlServer(connectionString).Options;
            return new StoreContext(options);
        }
    }
}
