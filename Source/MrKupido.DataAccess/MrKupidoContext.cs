using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MrKupido.Model;

namespace MrKupido.DataAccess
{
    public class MrKupidoContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ConversionRate> ConversionRates { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<FilterItem> FilterItems { get; set; }
        public DbSet<ImportedRecipe> ImportedRecipes { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<User> Users { get; set; }

        public MrKupidoContext() : this("Name=MrKupidoContext") { }

        public MrKupidoContext(string connectionStringId) : base(connectionStringId) {}
    }
}
