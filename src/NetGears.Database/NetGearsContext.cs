using Microsoft.EntityFrameworkCore;
using NetGears.Core.Configuration;
using NetGears.Database.Entities;

namespace NetGears.Database
{
    public class NetGearsContext : DbContext
    {
        private const string DatabaseConfigurationPath = "database.json";

        public DbSet<Account> Accounts { get; set; }

        public DatabaseConfiguration DatabaseConfiguration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DatabaseConfiguration = ConfigurationLoader.Instance.Load<DatabaseConfiguration>(DatabaseConfigurationPath);

            optionsBuilder.UseSqlServer($"Data Source={DatabaseConfiguration.Host};Initial Catalog={DatabaseConfiguration.DatabaseName};Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}