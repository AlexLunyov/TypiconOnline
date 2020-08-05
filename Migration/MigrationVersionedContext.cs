using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Repository.Versioned;

namespace TypiconMigrationTool
{
    public class MigrationVersionedContext : VersionedContext
    {
        public MigrationVersionedContext() : base(CreateOptions()) { }

        private static DbContextOptions<VersionedContext> CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VersionedContext>();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            //MSSql
            //DbOptions.CofigureMsSql(optionsBuilder);

            //Sqlite
            //DbOptions.ConfigureSqlite(optionsBuilder, config);
            optionsBuilder.UseSqlite("FileName=Data\\SQLiteVersionedDB.db");
            optionsBuilder.EnableSensitiveDataLogging();

            //PostgreSQL
            //DbOptions.CofigurePostgre(optionsBuilder);

            //MySQL
            //DbOptions.CofigureMySql(optionsBuilder);

            return optionsBuilder.Options;
        }
    }
}
