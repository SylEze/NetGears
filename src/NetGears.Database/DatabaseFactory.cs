using Microsoft.EntityFrameworkCore.Design;
using NetGears.Core.Logger;
using NetGears.Core.Misc;

namespace NetGears.Database
{
    public class DatabaseFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        private static readonly Logger Logger = Logger.GetLogger<DatabaseFactory>();
        
        private const string DatabaseConfigurationPath = "config/database.json";

        private static DatabaseConfiguration DatabaseConfiguration { get; set; }
        
        private static DatabaseContext DatabaseContext { get; set; }

        public static void Initialize()
        {
            if (DatabaseConfiguration != null)
            {
                throw new DatabaseException("Database factory already initialized.");
            }

            DatabaseConfiguration = JsonConfigurationLoader.Load<DatabaseConfiguration>(DatabaseConfigurationPath);
            
            DatabaseContext = new DatabaseContext(DatabaseConfiguration);

            Logger.Info("Database factory initialized.");
        }

        public static DatabaseContext GetNetGearsContext()
        {
            if (DatabaseConfiguration == null || DatabaseContext == null)
            {
                throw new DatabaseException("Database factory not initialized.");
            }

            return DatabaseContext;
        }

        public DatabaseContext CreateDbContext(string[] args)
        {
            var dbConfiguration = JsonConfigurationLoader.Load<DatabaseConfiguration>(DatabaseConfigurationPath);

            if (dbConfiguration == null)
            {
                throw new DatabaseException("Cannot load database configuration.");
            }

            return new DatabaseContext(dbConfiguration);
        }
    }
}
