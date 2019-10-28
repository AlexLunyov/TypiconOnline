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
    public class DbContext : TypiconDBContext
    {
        public DbContext() : base(CreateOptions()) { }

        private static DbContextOptions<TypiconDBContext> CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TypiconDBContext>();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            //MSSql
            //DbOptions.CofigureMsSql(optionsBuilder);

            //Sqlite
            DbOptions.ConfigureSqlite(optionsBuilder, config);

            //PostgreSQL
            //DbOptions.CofigurePostgre(optionsBuilder);

            //MySQL
            //DbOptions.CofigureMySql(optionsBuilder);

            return optionsBuilder.Options;
        }
    }
}
