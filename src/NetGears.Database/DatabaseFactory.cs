using System;
using Microsoft.EntityFrameworkCore.Design;
using NetGears.Core.Configuration;
using NetGears.Core.Logger;

namespace NetGears.Database
{
    public class DatabaseFactory : IDesignTimeDbContextFactory<NetGearsContext>
    {
        private const string DatabaseConfigurationPath = "database.json";

        private static DatabaseConfiguration DatabaseConfiguration { get; set; }

        public static void Initialize()
        {
            if (DatabaseConfiguration != null)
            {
                throw new DatabaseException("DatabaseHelper already initialised.");
            }

            DatabaseConfiguration = ConfigurationLoader.Load<DatabaseConfiguration>(DatabaseConfigurationPath);

            Logger.Info("Database configuration loaded.");
        }

        public static NetGearsContext GetNetGearsContext()
        {
            if (DatabaseConfiguration == null)
            {
                throw new DatabaseException("DatabaseHelper not initialised.");
            }

            return new NetGearsContext(DatabaseConfiguration);
        }

        public NetGearsContext CreateDbContext(string[] args)
        {
            var dbConfiguration = ConfigurationLoader.Load<DatabaseConfiguration>(DatabaseConfigurationPath);

            if (dbConfiguration == null)
            {
                throw new DatabaseException("Cannot load database configuration");
            }

            return new NetGearsContext(dbConfiguration);
        }
    }
}
