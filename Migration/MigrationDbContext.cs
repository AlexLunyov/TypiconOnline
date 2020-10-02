using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconMigrationTool
{
    public class MigrationDbContext : TypiconDBContext
    {
        public MigrationDbContext() : base(CreateOptions()) { }

        private static DbContextOptions<TypiconDBContext> CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            //MSSql
            //DbOptions.ConfigureMsSql(optionsBuilder);

            //Sqlite
            DbOptions.ConfigureSqlite(optionsBuilder, config);

            //PostgreSQL
            //DbOptions.ConfigurePostgre(optionsBuilder);

            //MySQL
            //DbOptions.ConfigureMySql(optionsBuilder, config);

            return optionsBuilder.Options;
        }
    }

    /// <summary>
    /// Контекст для миграций MySql бд
    /// </summary>
    public class MigrationMySqlDbContext : TypiconDBContext
    {
        public MigrationMySqlDbContext() : base(CreateOptions()) { }

        private static DbContextOptions<TypiconDBContext> CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            //MSSql
            //DbOptions.ConfigureMsSql(optionsBuilder);

            //Sqlite
            //DbOptions.ConfigureSqlite(optionsBuilder, config);

            //PostgreSQL
            //DbOptions.ConfigurePostgre(optionsBuilder);

            //MySQL
            DbOptions.ConfigureMySql(optionsBuilder, config);

            return optionsBuilder.Options;
        }
    }
}
