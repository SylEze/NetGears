﻿using System;
using Microsoft.EntityFrameworkCore;
using NetGears.Core.Configuration;
using NetGears.Core.Logger;
using NetGears.Database.Entities;
using NetGears.Database.Repositories;

namespace NetGears.Database
{
    public class NetGearsContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        
        public IRepository<Account> AccountRepository { get; }

        public DatabaseConfiguration DatabaseConfiguration { get; }

        public NetGearsContext(DatabaseConfiguration configuration)
        {
            DatabaseConfiguration = configuration;

            AccountRepository = new AccountRepository(this);
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
                        $"Server={DatabaseConfiguration.Host};Database={DatabaseConfiguration.DatabaseName};" +
                        $"Uid={DatabaseConfiguration.User};Pwd={DatabaseConfiguration.Password};");
                    break;
                default:
                    throw new NotImplementedException("The selected database provider is not yet implemented " +
                                                      "or does not exist. Please change your database.json");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}