using System;
using System.Collections.Generic;
using System.Text;
using NetGears.Core.Configuration;
using NetGears.Core.Logger;

namespace NetGears.Database
{
    public static class DatabaseHelper
    {
        private const string DatabaseConfigurationPath = "database.json";

        private static DatabaseConfiguration DatabaseConfiguration { get; set; }

        public static void Initialize()
        {
            if (DatabaseConfiguration != null)
            {
                throw new Exception("DatabaseHelper already initialised.");
            }

            DatabaseConfiguration = ConfigurationLoader.Instance.Load<DatabaseConfiguration>(DatabaseConfigurationPath);

            Logger.Info("Database configuration loaded.");
        }

        public static NetGearsContext GetNetGearsContext()
        {
            if (DatabaseConfiguration == null)
            {
                throw new Exception("DatabaseHelper not initialised.");
            }

            return new NetGearsContext(DatabaseConfiguration);
        }
    }
}
