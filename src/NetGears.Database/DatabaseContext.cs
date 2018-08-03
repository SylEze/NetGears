using System;
using Microsoft.EntityFrameworkCore;

namespace NetGears.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseConfiguration DatabaseConfiguration { get; }

        public DatabaseContext(DatabaseConfiguration configuration)
        {
            DatabaseConfiguration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (DatabaseConfiguration.DatabaseProvider)
            {
                case DatabaseProvider.SQLServer:
                    optionsBuilder.UseSqlServer(
                        $"Data Source={DatabaseConfiguration.Host};Initial Catalog={DatabaseConfiguration.DatabaseName};Integrated Security=true;");
                    break;
                case DatabaseProvider.MySQL:
                    optionsBuilder.UseMySql(
                        $"Server={DatabaseConfiguration.Host};Port={DatabaseConfiguration.Port};Database={DatabaseConfiguration.DatabaseName};" +
                        $"Uid={DatabaseConfiguration.User};Pwd={DatabaseConfiguration.Password};");
                    break;
                default:
                    throw new NotImplementedException("The selected database provider is not yet implemented " +
                                                      "or does not exist. Please change your database.json");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}