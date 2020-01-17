using Microsoft.EntityFrameworkCore;

namespace DAL
{
    // we inject this factory instead of context, and that way we have an instance
    // for each repo (easier with mvvm light simpleioc limitations.
    public class StoreContextFactory
    {
        DbContextOptions options;
        public StoreContextFactory(DbContextOptions options)
        {
            this.options = options;
        }
        public StoreContext GetContext()
        {
            return new StoreContext(options);
        }
    }
}
