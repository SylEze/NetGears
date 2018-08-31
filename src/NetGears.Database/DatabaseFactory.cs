using Microsoft.EntityFrameworkCore.Design;
using NetGears.Core.Logger;
using NetGears.Core.Misc;
using System.IO;

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
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/bin/config/database.json";
            var dbConfiguration = JsonConfigurationLoader.Load<DatabaseConfiguration>(path);

            if (dbConfiguration == null)
            {
                throw new DatabaseException($"{path} Cannot load database configuration.");
            }

            return new DatabaseContext(dbConfiguration);
        }
    }
}
