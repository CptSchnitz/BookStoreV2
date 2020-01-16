using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
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
