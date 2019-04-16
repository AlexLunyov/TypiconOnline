using Microsoft.EntityFrameworkCore;
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

            //MSSql
            //DbOptions.CofigureMsSql(optionsBuilder);

            //Sqlite
            DbOptions.CofigureSqlite(optionsBuilder);

            //PostgreSQL
            //DbOptions.CofigurePostgre(optionsBuilder);

            //MySQL
            //DbOptions.CofigureMySql(optionsBuilder);

            return optionsBuilder.Options;
        }
    }
}
